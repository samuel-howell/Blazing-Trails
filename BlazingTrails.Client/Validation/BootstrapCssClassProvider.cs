﻿using Microsoft.AspNetCore.Components.Forms;

namespace BlazingTrails.Client.Validation
{
    public class BootstrapCssClassProvider : FieldCssClassProvider
    {
        public override string GetFieldCssClass(EditContext editContext, in FieldIdentifier fieldIdentifier)
        {
            var isValid = !editContext.GetValidationMessages(fieldIdentifier).Any(); // Check if the current field has any validation errors and set isValid appropriately


            if (editContext.IsModified(fieldIdentifier)) 
            {
                return isValid ? "is-valid " : "is-invalid"; // The field has been modified. Return custom CSS classes depending on whether the field isvalid or not
            }

            return isValid ? "" : "is-invalid"; // The field has not been modified. Return a custom CSS class if the field is invalid but not if it’s valid.
        }
    }
}