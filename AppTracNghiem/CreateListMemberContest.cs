using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppTracNghiem.Models;
using AppTracNghiem.Services;
using AppTracNghiem.Helpers;

namespace AppTracNghiem
{
    public partial class CreateListMemberContest : Form
    {
        private List<UserModel> membersList;
        private string _contestId;
        private ContestService _contestService;

        public CreateListMemberContest(string contestId)
        {
            InitializeComponent();
            _contestId = contestId;
            _contestService = new ContestService();
            membersList = new List<UserModel>();
            SetupDataGrid();
            SetupEventHandlers();
        }

        private void SetupDataGrid()
        {
            dataGridViewMembers.Columns.Clear();
            dataGridViewMembers.Columns.Add("FullName", "Họ và Tên");
            dataGridViewMembers.Columns.Add("Username", "MSSV");
            dataGridViewMembers.Columns.Add("SchoolEmail", "Email");
            dataGridViewMembers.Columns.Add("ClassName", "Lớp");
            dataGridViewMembers.Columns.Add("Id", "ID");
            dataGridViewMembers.Columns["Id"].Visible = false;
        }

        private void SetupEventHandlers()
        {
            materialButton3.Click += MaterialButton3_Click;
            materialButton4.Click += MaterialButton4_Click;
            materialButton2.Click += MaterialButton2_Click;
        }

        private void CreateListMemberContest_Load(object sender, EventArgs e)
        {
            LoadMembersFromServer();
        }

        private async void LoadMembersFromServer()
        {
            try
            {
                var tokenData = TokenManager.GetTokenData();
                if (tokenData == null) return;

                var members = await _contestService.GetContestMembersAsync(_contestId, tokenData.AccessToken);
                membersList.Clear();
                
                foreach (var member in members)
                {
                    var user = new UserModel
                    {
                        Id = member.userId.ToString(),
                        Username = member.studentId.ToString(),
                        FullName = member.fullName.ToString(),
                        SchoolEmail = member.email.ToString(),
                        ClassName = member.class_name?.ToString()
                    };
                    membersList.Add(user);
                }
                
                RefreshDataGrid();
            }
            catch { }
        }

        private void MaterialButton3_Click(object? sender, EventArgs e)
        {
            MessageBox.Show(
                "Chức năng này hiện tại đang phát triển.",
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private async void MaterialButton4_Click(object? sender, EventArgs e)
        {
            if (dataGridViewMembers.SelectedRows.Count > 0)
            {
                int selectedIndex = dataGridViewMembers.SelectedRows[0].Index;
                var member = membersList[selectedIndex];
                
                var result = MessageBox.Show(
                    $"Xóa {member.FullName} khỏi danh sách?",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        var tokenData = TokenManager.GetTokenData();
                        if (tokenData != null)
                        {
                            var success = await _contestService.RemoveMemberFromContestAsync(
                                _contestId, member.Id, tokenData.AccessToken);
                            
                            if (success)
                            {
                                membersList.RemoveAt(selectedIndex);
                                RefreshDataGrid();
                            }
                            else
                            {
                                MessageBox.Show("Lỗi khi xóa thành viên!", 
                                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi: {ex.Message}", 
                            "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private async void MaterialButton2_Click(object? sender, EventArgs e)
        {
            var addMemberForm = new AddMemberForm();
            if (addMemberForm.ShowDialog() == DialogResult.OK)
            {
                var selectedUser = addMemberForm.GetSelectedUser();
                if (selectedUser != null)
                {
                    if (!membersList.Any(m => m.Id == selectedUser.Id))
                    {
                        try
                        {
                            var tokenData = TokenManager.GetTokenData();
                            if (tokenData != null)
                            {
                                var success = await _contestService.AddMemberToContestAsync(
                                    _contestId,
                                    selectedUser.Id,
                                    selectedUser.Username,
                                    selectedUser.FullName,
                                    selectedUser.SchoolEmail,
                                    selectedUser.ClassName ?? "",
                                    tokenData.AccessToken);
                                
                                if (success)
                                {
                                    membersList.Add(selectedUser);
                                    RefreshDataGrid();
                                }
                                else
                                {
                                    MessageBox.Show("Lỗi khi thêm thành viên!", 
                                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Lỗi: {ex.Message}", 
                                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Sinh viên này đã có trong danh sách", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void RefreshDataGrid()
        {
            dataGridViewMembers.Rows.Clear();
            foreach (var member in membersList)
            {
                dataGridViewMembers.Rows.Add(
                    member.FullName,
                    member.Username,
                    member.SchoolEmail,
                    member.ClassName ?? "N/A",
                    member.Id
                );
            }
        }

        private void CreateListMemberContest_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
    }
}
