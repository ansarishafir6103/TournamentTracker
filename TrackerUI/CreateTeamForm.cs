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
using TrackerLibrary.Models;

namespace TrackerUI
{
    public partial class CreateTeamForm : Form
    {
        #region ListItems
        private List<PersonModel> availableTeamMembers = GlobalConfig.Connection.GetPerson_All();
        private  List<PersonModel> selectedTeamMembers = new List<PersonModel>();
        #endregion

        #region Constructors
        private ITeamRequester CallingForm;
        public CreateTeamForm(ITeamRequester caller)
        {
            InitializeComponent();

            //CreateSampleData();

            CallingForm = caller;

            WireUpLists();
        }
        #endregion

        #region Methods
        private void LoadListData()
        {
            availableTeamMembers = GlobalConfig.Connection.GetPerson_All();
        }
        private void CreateSampleData()
        {

            availableTeamMembers.Add(new PersonModel {FirstName="Tim",LastName="Corey" });
            availableTeamMembers.Add(new PersonModel { FirstName = "Sue", LastName = "Strom" });

            selectedTeamMembers.Add(new PersonModel { FirstName = "Jane", LastName = "Smith" });
            selectedTeamMembers.Add(new PersonModel {FirstName="Bill",LastName="Jones" });
        }
        private void WireUpLists()
        {
            selectTeamMemberDropDown.DataSource = null;

            selectTeamMemberDropDown.DataSource = availableTeamMembers;
            selectTeamMemberDropDown.DisplayMember = "FullName";

            teamMemberListBox.DataSource = null;

            teamMemberListBox.DataSource = selectedTeamMembers;
            teamMemberListBox.DisplayMember = "FullName";
        }
        private bool ValidateForm()
        {
            // TODO - Add validation to the form.
            if (firstNameVale.Text.Length==0)
            {
                return false;
            }
            if (lastNameValue.Text.Length==0)
            {
                return false;
            }
            if (emailValue.Text.Length==0)
            {
                return false;
            }
            if (cellPhoneValue.Text.Length==0)
            {
                return false;
            }
            return true;
        }
        #endregion

        #region Events
        private void addMemberButton_Click(object sender, EventArgs e)
        {
            PersonModel p = (PersonModel)selectTeamMemberDropDown.SelectedItem;

            if (p!=null)
            {
                availableTeamMembers.Remove(p);
                selectedTeamMembers.Add(p);

                WireUpLists();
            }
        }
        private void removeSelectedMemberButton_Click(object sender, EventArgs e)
        {
            PersonModel p = (PersonModel)teamMemberListBox.SelectedItem;

            if (p!=null)
            {
                selectedTeamMembers.Remove(p);
                availableTeamMembers.Add(p);

                WireUpLists();
            }
        }
        private void createMemberButton_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                PersonModel p = new PersonModel();
                p.FirstName = firstNameVale.Text;
                p.LastName = lastNameValue.Text;
                p.EmailAddress = emailValue.Text;
                p.CellPhoneNumber = cellPhoneValue.Text;

                p= GlobalConfig.Connection.CreatePreson(p);

                if (p!=null)
                {
                    availableTeamMembers.Remove(p);
                    selectedTeamMembers.Add(p);

                    WireUpLists();

                    firstNameVale.Text = "";
                    lastNameValue.Text = "";
                    emailValue.Text = "";
                    cellPhoneValue.Text = "";
                }
            }
            else
            {
                MessageBox.Show("You need to fill in all of the fiels.");
            }
        }

        private void createteamButton_Click(object sender, EventArgs e)
        {
            TeamModel t = new TeamModel();

            t.TeamName = teamNameValue.Text;
            t.TeamMembers = selectedTeamMembers;

            GlobalConfig.Connection.CreateTeam(t);

            CallingForm.TeamComplete(t);

            this.Close();
            // TODO - if we aren't closing this form after creation reset the form
        }

        #endregion


    }
}
