<%@ Page Title="" Language="C#" MasterPageFile="~/FitnessMstr.master" AutoEventWireup="true" CodeFile="UserBookedList.aspx.cs" Inherits="Master_WorkOutPlan_UserBookedList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .empdtls {
            margin-bottom: 1.7rem;
            color: #202124;
            font-size: 1rem;
        }

        .ddl {
            padding: 1rem;
            border: 1px dashed;
            margin-bottom: 0.5rem;
            border-radius: 1rem;
        }

        .profile {
            margin-bottom: 0.5rem;
            color: #202124;
            font-size: 1rem;
            margin-left: 2rem;
        }

        .imgpreview {
            width: 100px;
            height: 100px;
            margin-bottom: 0.2rem;
            border-radius: 4rem;
            margin-left: 1rem;
            margin-top: 1rem;
            border: 1px solid;
        }

        .GvHead {
            text-align: center;
            font-size: 15px;
        }
    </style>

    <div class="PageRoute">
        <div>
            <asp:Label ID="lblMainPage" CssClass="pageRoutecol" runat="server" Text="Home"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="lblNavFirst" runat="server" CssClass="pageRoutecol" Text="Tools"></asp:Label>
            <i class="fafaicon">/</i>

            <asp:Label ID="lblNav3" runat="server" CssClass="pageRoutecol" Text="Diet and Workout Tool"></asp:Label>
        </div>
    </div>
    <div class="container-fluid frmcontainer">
        <div id="divGv" runat="server">
            <div class="row">
                <div class="PageHeader">
                    <h4>Booking <span>List</span></h4>


                </div>

            </div>
            <div class="row">

                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                    <div class="txtboxdiv">
                        <asp:TextBox ID="txtFromDate" CssClass="txtbox fromDate" TabIndex="1" runat="server" placeholder=" " />
                        <asp:Label CssClass="txtlabel" runat="server">Date<span class="reqiredstar">*</span></asp:Label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="TranDietOutPlan"
                            ControlToValidate="txtFromDate" runat="server" CssClass="rfvStyle"
                            ErrorMessage="Enter From Date">
                        </asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">

                    <asp:DropDownList ID="ddlGenerateType" runat="server" TabIndex="4" CssClass="form-select" RepeatDirection="Horizontal"
                        OnSelectedIndexChanged="ddlGenerateType_SelectedIndexChanged" AutoPostBack="true">
                    <asp:ListItem Value="0">
                        Type *
                    </asp:ListItem>
                        <asp:ListItem Value="N" >Not Generated</asp:ListItem>
                        <asp:ListItem Value="A">Generated</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="TranDietOutPlan"
                        ControlToValidate="ddlGenerateType" runat="server" CssClass="rfvStyle" InitialValue="0"
                        ErrorMessage="Select Diet Type Name">
                    </asp:RequiredFieldValidator>
                </div>
            </div>

            <div id="divGridView" runat="server" class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 table-responsive">
                <asp:GridView ID="gvUserBookedList" runat="server" AutoGenerateColumns="false"
                    CssClass="table table-striped table-hover border display gvFilter" Style="font-size: 0.8rem"
                    OnRowDataBound="gvUserBookedList_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="S.no" HeaderStyle-CssClass="GvHead">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="User Name <br/> Phone No.">
                            <ItemTemplate>
                                <asp:Label ID="lbluserId" runat="server" Text='<%#Bind("userId") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblbookingId" runat="server" Text='<%#Bind("bookingId") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lbluserName" runat="server" Text='<%#Bind("userName") %>'> </asp:Label>
                                <br />
                                <asp:Label ID="lblPhoneNumber" runat="server" Text='<%#Bind("phoneNumber") %>'> </asp:Label>
                                <asp:Label ID="lblMailId" runat="server" Text='<%#Bind("mailId") %>' Visible="false"> </asp:Label>
                                <asp:Label ID="lblTDEE" runat="server" Text='<%#Bind("TDEE") %>' Visible="false"> </asp:Label>
                                <asp:Label ID="lblapprovedStatus" runat="server" Text='<%#Bind("approvedStatus") %>' Visible="false"> </asp:Label>
                                <asp:Label ID="lblPlanGenearetedDiet" runat="server" Text='<%#Bind("PlanGenearetedDiet") %>' Visible="false"> </asp:Label>
                                <asp:Label ID="lblPlanGeneareted" runat="server" Text='<%#Bind("PlanGeneareted") %>' Visible="false"> </asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Category Name <br/> Training Type">
                            <ItemTemplate>
                                <asp:Label ID="lblcategoryId" runat="server" Text='<%#Bind("categoryId") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblcategoryName" runat="server" Text='<%#Bind("categoryName") %>'> </asp:Label>
                                <br />
                                <asp:Label ID="lbltrainingTypeId" runat="server" Text='<%#Bind("trainingTypeId") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lbltrainingTypeName" runat="server" Text='<%#Bind("trainingTypeName") %>'> </asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Plan Duration">
                            <ItemTemplate>
                                <asp:Label ID="lblplanDuration" runat="server" Text='<%#Bind("planDuration") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblplanDurationName" runat="server" Text='<%#Bind("planDurationName") %>'> </asp:Label>
                                <asp:Label ID="lblfromDate" runat="server" Text='<%#Bind("fromDate") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lbltoDate" runat="server" Text='<%#Bind("toDate") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Generate Plan">
                            <ItemTemplate>
                                <asp:Label ID="lblGeneareted" runat="server" Text="Generated" Visible="false"> </asp:Label>
                                <asp:Button ID="btngenerate" Text='<%#Eval("paymentStatus").ToString().Trim() =="P"?"Generate":"Not Paid"%>' CssClass="btnAdd" 
                                    Enabled='<%#Eval("paymentStatus").ToString().Trim() =="P"?true:false%>'
                                    runat="server" OnClick="btngenerate_Click" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Approve Plan">
                            <ItemTemplate>
                                <asp:Label ID="lblApproved" runat="server" Text="Approved" Visible="false"> </asp:Label>
                                <asp:Button ID="btnApprove" Text="Approve" CssClass="btnAdd" runat="server" OnClick="btnApprove_Click" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="View">
                            <ItemTemplate>
                                <asp:LinkButton ID="LnkView" runat="server" OnClick="LnkView_Click">
                                    <i class="fa fa-eye" style="color:black"></i>
                                </asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Send Sms/ Mail">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnSendSmsandMail" ImageUrl="../../img/Mail.png"
                                    runat="server" OnClick="btnSendSmsandMail_Click" Width="35"
                                    Visible='<%#Eval("approvedStatus").ToString().Trim() =="A"?true:false%>' />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>

    </div>



</asp:Content>

