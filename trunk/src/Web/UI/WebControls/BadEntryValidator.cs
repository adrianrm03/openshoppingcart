using System;

namespace Serialcoder.ShoppingCart.Web.UI.WebControls
{
	/// <summary>
	/// Summary description for BadEntryValidator.
	/// </summary>
	public class BadEntryValidator : System.Web.UI.IValidator
	{
		private bool isValid;
		private string message;

		public BadEntryValidator(string message)
		{
			isValid = false;
			this.message = message;
		}

		#region IValidator Members

		public void Validate()
		{
		}

		public bool IsValid
		{
			get
			{
				return isValid;
			}
			set
			{
				isValid = value;
			}
		}

		public string ErrorMessage
		{
			get
			{
				return message;
			}
			set
			{
				message = value;
			}
		}

		#endregion
	}
}
