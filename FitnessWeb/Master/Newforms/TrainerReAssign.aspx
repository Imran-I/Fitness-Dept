<%@ Page Title="" Language="C#" MasterPageFile="~/FitnessMstr.master" AutoEventWireup="true" CodeFile="TrainerReAssign.aspx.cs" Inherits="Master_Branch_TrainerUserAssign" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="PageRoute">
        <div>
            <asp:Label ID="lblMainPage" CssClass="pageRoutecol" runat="server" Text="Home"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="lblNavFirst" runat="server" CssClass="pageRoutecol" Text="Fitness Category"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="lblNavSecond" runat="server" CssClass="pageRoutecol" Text="Trainer Reassign"></asp:Label>
        </div>
    </div>
    <div class="container-fluid frmcontainer">
        <div id="DivForm" runat="server" visible="false">
            <div class="PageHeader">
                <h5>Trainer <span>Reassign</span></h5>
            </div>

            <div class="row">

                <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                    <asp:DropDownList ID="ddlOldTrainer"
                        CssClass="form-select" runat="server" TabIndex="5" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlOldTrainer_SelectedIndexChanged">
                    </asp:DropDownList>

                </div>
                <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                    <asp:DropDownList ID="ddlCategory"
                        CssClass="form-select" runat="server" TabIndex="5" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                    </asp:DropDownList>

                </div>
                <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                    <asp:DropDownList ID="ddlTrainingType"
                        CssClass="form-select" runat="server" TabIndex="5" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlTrainingType_SelectedIndexChanged">
                    </asp:DropDownList>

                </div>
            </div>
            <div class="row">
                <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 mb-3" id="divSlotList" runat="server" visible="false">
                    <div>
                        <asp:Label
                            ID="Label1"
                            runat="server">Slots<span class="reqiredstar">*</span>
                        </asp:Label>
                    </div>
                    <asp:CheckBoxList ID="chkSlotList" runat="server" RepeatDirection="Horizontal" RepeatColumns="4"
                        AutoPostBack="true" OnSelectedIndexChanged="chkSlotList_SelectedIndexChanged">
                    </asp:CheckBoxList>
                </div>
            </div>
            <div class="row">
                <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                    <asp:DropDownList ID="ddlNewTrainer"
                        CssClass="form-select" runat="server" TabIndex="5">
                    </asp:DropDownList>

                </div>
                <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                    <div class="txtboxdiv">
                        <asp:TextBox ID="txtFromDate" TabIndex="19" CssClass="txtbox ConvertfromDate" runat="server" placeholder=" " />
                        <asp:Label CssClass="txtlabel" runat="server"> From Date  <span class="reqiredstar">*</span></asp:Label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10"
                            ControlToValidate="txtFromDate" runat="server" CssClass="rfvStyle" ValidationGroup="MstrEmp"
                            ErrorMessage="Enter Date">
                        </asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                    <div class="txtboxdiv">
                        <asp:TextBox ID="txtToDate" TabIndex="19" CssClass="txtbox ConvertfromDate" runat="server" placeholder=" " />
                        <asp:Label CssClass="txtlabel" runat="server"> To Date  <span class="reqiredstar">*</span></asp:Label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                            ControlToValidate="txtToDate" runat="server" CssClass="rfvStyle" ValidationGroup="MstrEmp"
                            ErrorMessage="Enter Date">
                        </asp:RequiredFieldValidator>
                    </div>
                </div>

            </div>

            <div class="float-end">
                <asp:Button CssClass="btnSubmit" ID="btnSubmit" ValidationGroup="FCSlot" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                <asp:Button ID="btnCancel" CssClass="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
            </div>
        </div>

        <div id="divGv" runat="server">
            <div class="row">
                <div class="PageHeader">
                    <h4>Category  <span>Slot</span></h4>

                    <div class="float-end">


                        <asp:LinkButton ID="btnAdd" runat="server" OnClick="btnAdd_Click"
                            CssClass="btnAdd">&nbsp;   <i class="fa fa-plus AddPlus"></i>  Add</asp:LinkButton>
                    </div>
                </div>
            </div>

            <div id="divGridView" runat="server" class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 table-responsive">
                <asp:GridView ID="gvTrainerReassign" runat="server" DataKeyNames="oldTrainerId,newTrainerId" AutoGenerateColumns="false"
                    CssClass="table table-striped table-hover border display gvFilter">
                    <Columns>
                        <asp:TemplateField HeaderText="Sno">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Old Tranier Name" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lbloldTrainerId" runat="server" Text='<%#Bind("oldTrainerId") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lbloldTrainerName" runat="server" Text='<%#Bind("oldTrainerName") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="New Tranier Name" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblnewTrainerId" runat="server" Text='<%#Bind("newTrainerId") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblnewTrainerName" runat="server" Text='<%#Bind("newTrainerName") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Category" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblcategoryId" runat="server" Text='<%#Bind("categoryId") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblcategoryName" runat="server" Text='<%#Bind("categoryName") %>' Visible="true"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="TrainingType" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lbltrainingTypeId" runat="server" Text='<%#Bind("trainingTypeId") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lbltrainingTypeName" runat="server" Text='<%#Bind("trainingTypeName") %>' Visible="true"></asp:Label>

                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Slot" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblslotId" runat="server" Text='<%#Bind("slotId") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblSlotTime" runat="server" Text='<%#Bind("SlotTime") %>' Visible="true"></asp:Label>

                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblDate" runat="server" Text='<%#Bind("Date")%>' Visible="true"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>



    </div>

</asp:Content>

