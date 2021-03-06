﻿//
// THIS WORK IS LICENSED UNDER A CREATIVE COMMONS ATTRIBUTION-NONCOMMERCIAL-
// SHAREALIKE 3.0 UNPORTED LICENSE:
// http://creativecommons.org/licenses/by-nc-sa/3.0/
//

using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Rock.Web.UI.Controls
{   
    /// <summary>
    /// A <see cref="T:System.Web.UI.WebControls.TextBox"/> control with an associated label.
    /// </summary>
    [ToolboxData( "<{0}:LabeledTextBox runat=server></{0}:LabeledTextBox>" )]
    public class LabeledTextBox : TextBox
    {
        private Label label;
        private RequiredFieldValidator validator;

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="LabeledTextBox"/> is required.
        /// </summary>
        /// <value>
        ///   <c>true</c> if required; otherwise, <c>false</c>.
        /// </value>
        [
        Bindable( true ),
        Category( "Behavior" ),
        DefaultValue( "false" ),
        Description( "Is the value required?" )
        ]
        public bool Required
        {
            get
            {
                if ( ViewState["Required"] != null )
                    return ( bool )ViewState["Required"];
                else
                    return false;
            }
            set
            {
                ViewState["Required"] = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [display required indicator].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [display required indicator]; otherwise, <c>false</c>.
        /// </value>
        [
        Bindable( true ),
        Category( "Appearance" ),
        DefaultValue( "true" ),
        Description( "Should the required indicator be displayed?" )
        ]
        public bool DisplayRequiredIndicator
        {
            get
            {
                if ( ViewState["DisplayRequiredIndicator"] != null )
                    return ( bool )ViewState["DisplayRequiredIndicator"];
                else
                    return true;
            }
            set
            {
                ViewState["DisplayRequiredIndicator"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the help tip.
        /// </summary>
        /// <value>
        /// The help tip.
        /// </value>
        [
        Bindable( true ),
        Category( "Appearance" ),
        DefaultValue( "" ),
        Description( "The help tip." )
        ]
        public string Tip
        {
            get
            {
                string s = ViewState["Tip"] as string;
                return s == null ? string.Empty : s;
            }
            set
            {
                ViewState["Tip"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the help block.
        /// </summary>
        /// <value>
        /// The help block.
        /// </value>
        [
        Bindable( true ),
        Category( "Appearance" ),
        DefaultValue( "" ),
        Description( "The help block." )
        ]
        public string Help
        {
            get
            {
                string s = ViewState["Help"] as string;
                return s == null ? string.Empty : s;
            }
            set
            {
                ViewState["Help"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the label text.
        /// </summary>
        /// <value>
        /// The label text.
        /// </value>
        [
        Bindable( true ),
        Category( "Appearance" ),
        DefaultValue( "" ),
        Description( "The text for the label." )
        ]
        public string LabelText
        {
            get
            {
                EnsureChildControls();
                return label.Text;
            }
            set
            {
                EnsureChildControls();
                label.Text = value;
            }
        }

        /// <summary>
        /// Renders a label and <see cref="T:System.Web.UI.WebControls.TextBox"/> control to the specified <see cref="T:System.Web.UI.HtmlTextWriter"/> object.
        /// </summary>
        /// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter"/> that receives the rendered output.</param>
        protected override void Render( HtmlTextWriter writer )
        {
            bool isValid = !Required || validator.IsValid;

            writer.AddAttribute( "class", isValid ? "" : "error" );
            writer.RenderBeginTag( HtmlTextWriterTag.Dl );

            writer.RenderBeginTag( HtmlTextWriterTag.Dt );
            if ( Required && DisplayRequiredIndicator )
                writer.AddAttribute( "class", "required" );
            label.RenderControl( writer );
            writer.RenderEndTag();

            writer.RenderBeginTag( HtmlTextWriterTag.Dd );
            base.Render( writer );

            if ( Required )
            {
                validator.ErrorMessage = LabelText + " is Required.";
                validator.RenderControl( writer );
            }

            if ( Tip.Trim() != string.Empty )
            {
                writer.AddAttribute( "class", "help-tip" );
                writer.AddAttribute( "href", "#" );
                writer.RenderBeginTag( HtmlTextWriterTag.A );
                writer.Write( "help" );
                writer.RenderBeginTag( HtmlTextWriterTag.Span );
                writer.Write( Tip.Trim() );
                writer.RenderEndTag();
                writer.RenderEndTag();
            }

            if ( Help.Trim() != string.Empty )
            {
                writer.AddAttribute( "class", "help-block" );
                writer.RenderBeginTag( HtmlTextWriterTag.Span );
                writer.Write( Tip.Trim() );
                writer.RenderEndTag();
            }

            writer.RenderEndTag();

            writer.RenderEndTag();
        }

        /// <summary>
        /// Called by the ASP.NET page framework to notify server controls that use composition-based implementation to create any child controls they contain in preparation for posting back or rendering.
        /// </summary>
        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            Controls.Clear();

            label = new Label();
            label.AssociatedControlID = this.ID;

            validator = new RequiredFieldValidator();
            validator.ID = this.ID + "_rfv";
            validator.ControlToValidate = this.ID;
            validator.Display = ValidatorDisplay.Dynamic;
            validator.CssClass = "validation-error";

            Controls.Add( label );
            Controls.Add( validator );
        }
    }
}