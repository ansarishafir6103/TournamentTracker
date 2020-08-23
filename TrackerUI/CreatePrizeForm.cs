using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrackerLibrary;
using TrackerLibrary.DataAccess;
using TrackerLibrary.Models;

namespace TrackerUI
{
    public partial class CreatePrizeForm : Form
    {

        #region Constructors
        private IPrizeRequester callingForm;
        public CreatePrizeForm(IPrizeRequester caller)
        {
            InitializeComponent();

            callingForm = caller;
        }

        #endregion

        #region Events
        private void createPrizeButton_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                PrizeModel model = new PrizeModel
                    (
                    placeNameValue.Text,
                    placeNumberValue.Text,
                    prizeAmountValue.Text,
                    prizePercentageValue.Text
                    );

                GlobalConfig.Connection.CreatePrize(model);

                callingForm.PrizeComplete(model);

                this.Close();

                //placeNameValue.Text = "";
                //placeNumberValue.Text = "";
                //prizeAmountValue.Text = "0";
                //prizePercentageValue.Text = "0";
            }
            else
            {
                MessageBox.Show("this form has invalid information.Please check it and try again.","Prize From",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Methods
        private bool ValidateForm()
        {
            bool output = true;

            int placeNumber = 0;

            bool placeNumberValidNumber = int.TryParse(placeNumberValue.Text, out placeNumber);

            if (!placeNumberValidNumber)
            {
                return output = false;
            }
            if (placeNumber < 1)
            {
                return output = false;
            }
            if (placeNumberValue.Text.Length == 0)
            {
                return output = false;
            }

            decimal prizeAmount = 0;
            int prizePercentage = 0;

            bool prizeAmountValid = decimal.TryParse(prizeAmountValue.Text, out prizeAmount);
            bool prizePercentageValid = int.TryParse(prizePercentageValue.Text, out prizePercentage);

            if (!prizeAmountValid || !prizePercentageValid)
            {
                return output = false;
            }
            if (prizeAmount <= 0 && prizePercentage <= 0)
            {
                return output = false;
            }
            if (prizePercentage < 0 || prizePercentage > 100)
            {
                return output = false;
            }

            return output;
        }

        #endregion
    }
}
