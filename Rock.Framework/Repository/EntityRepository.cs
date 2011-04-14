﻿using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

using Rock.Models;
using Rock.Helpers;

namespace Rock.Repository
{
    public class EntityRepository<T> : IRepository<T> where T : class
    {
        private DbContext _context;

        internal DbContext Context
        {
            get
            {
                if ( UnitOfWorkScope.CurrentObjectContext != null )
                    return UnitOfWorkScope.CurrentObjectContext;

                if ( _context == null )
                    _context = new Rock.EntityFramework.RockContext();

                return _context;
            }
        }

        internal DbSet<T> _objectSet;

        public EntityRepository() :
            this( new Rock.EntityFramework.RockContext() )
        { }

        public EntityRepository( DbContext objectContext )
        {
            _context = objectContext;
            _objectSet = Context.Set<T>();
        }

        public IQueryable<T> AsQueryable()                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           
        {
            return _objectSet;
        }

        public IEnumerable<T> GetAll()
        {
            return _objectSet.ToList();
        }

        public IEnumerable<T> Find( Expression<Func<T, bool>> where )
        {
            return _objectSet.Where( where );
        }

        public T Single( Expression<Func<T, bool>> where )
        {
            return _objectSet.Single( where );
        }

        public T First( Expression<Func<T, bool>> where )
        {
            return _objectSet.First( where );
        }

        public T FirstOrDefault( Expression<Func<T, bool>> where )
        {
            return _objectSet.FirstOrDefault( where );
        }

        public void Delete( T entity )
        {
            _objectSet.Remove( entity );
        }

        public void Add( T entity )
        {
            _objectSet.Add( entity );
        }

        public void Attach( T entity )
        {
            _objectSet.Attach( entity );
        }

        /// <summary>
        /// Saves the entity and returns a list of any entity changes that 
        /// need to be logged
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<Rock.Models.Core.EntityChange> Save( T entity, int? PersonId )
        {
            List<Rock.Models.Core.EntityChange> entityChanges = null;

            // Do not track changes on the 'EntityChange' entity type. 
            if ( !( entity is Rock.Models.Core.EntityChange ) )
            {
                Type entityType = entity.GetType();

                Guid changeSet = Guid.NewGuid();

                // Look for properties that have the "TrackChanges" attribute
                foreach ( PropertyInfo propInfo in entity.GetType().GetProperties() )
                {
                    if ( TrackChanges( propInfo.GetCustomAttributes( true ) ) )
                    {
                        var currentValue = Context.Entry( entity ).Property( propInfo.Name ).CurrentValue;
                        var originalValue = Context.Entry( entity ).State != System.Data.EntityState.Added ?
                            Context.Entry( entity ).Property( propInfo.Name ).OriginalValue : string.Empty;

                        if ( currentValue.ToString() != originalValue.ToString() )
                        {
                            if ( entityChanges == null )
                                entityChanges = new List<Models.Core.EntityChange>();

                            Rock.Models.Core.EntityChange change = new Models.Core.EntityChange();
                            change.ChangeSet = changeSet;
                            change.ChangeType = Context.Entry( entity ).State.ToString();
                            change.EntityType = entityType.Name;
                            change.Property = propInfo.Name;
                            change.OriginalValue = originalValue.ToString();
                            change.CurrentValue = currentValue.ToString();

                            entityChanges.Add( change );
                        }
                    }
                }
            }

            if (entity is IAuditable)
            {
                IAuditable auditable = (IAuditable)entity;

                if (Context.Entry(entity).State == System.Data.EntityState.Added)
                {
                    auditable.CreatedByPersonId = PersonId;
                    auditable.CreatedDateTime = DateTime.Now;
                }

                auditable.ModifiedByPersonId = PersonId;
                auditable.ModifiedDateTime = DateTime.Now;
            }

            Context.SaveChanges();

            return entityChanges;
        }

        private static bool TrackChanges( Object[] customAttributes )
        {
            foreach ( object attribute in customAttributes )
                if ( attribute is Rock.Models.TrackChangesAttribute )
                    return true;
            return false;
        }
    }
}