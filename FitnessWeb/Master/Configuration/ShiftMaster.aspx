<%@ Page Title="" Language="C#" MasterPageFile="~/FitnessMstr.master" AutoEventWireup="true" CodeFile="ShiftMaster.aspx.cs" Inherits="Master_GymOwner_ShiftMaster" %>

<asp:Content ID="MstrShift" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="PageRoute">
        <div>
            <asp:Label
                ID="lblMainPage"
                CssClass="pageRoutecol"
                runat="server"
                Text="Home">
            </asp:Label>
            <i class="fafaicon">/</i>

            <asp:Label
                ID="lblNavFirst"
                runat="server"
                CssClass="pageRoutecol"
                Text="Gym Setup">
            </asp:Label>
            <i class="fafaicon">/</i>
            
            <asp:Label
                ID="Label1"
                runat="server"
                CssClass="pageRoutecol"
                Text="Employee Setup">
            </asp:Label>
            <i class="fafaicon">/</i>

            <asp:Label
                ID="lblNavSecond"
                runat="server"
                CssClass="pageRoutecol"
                Text="Shift Master">
            </asp:Label>
        </div>
    </div>


    <div class="container-fluid frmcontainer">
        <%-- Form Side--%>
        <div id="DivForm" runat="server">
            <div class="PageHeader">
                <h5>Shift <span>Master</span></h5>
            </div>

            <div class="row">
                <%-- Shift Name --%>
                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                    <div class="txtboxdiv">
                        <asp:TextBox
                            ID="txtShiftName"
                            CssClass="txtbox"
                            runat="server"
                            TabIndex="1"
                            MaxLength="100"
                            AutoComplete="off"
                            placeholder=" " />
                        <asp:Label
                            CssClass="txtlabel"
                            runat="server">
                        Shift Name <span class="reqiredstar">*</span>
                        </asp:Label>
                    </div>
                    <asp:RequiredFieldValidator
                        ID="RfvtxtShiftName"
                        ValidationGroup="ShiftMstr"
                        ControlToValidate="txtShiftName"
                        runat="server"
                        CssClass="rfvStyle"
                        ErrorMessage="Enter Shift Name">
                    </asp:RequiredFieldValidator>
                </div>
                <%-- Grace Period --%>
                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                    <div class="txtboxdiv">
                        <asp:TextBox
                            ID="txtGracePeriod"
                            CssClass="txtbox"
                            type="number"
                            min="0"
                            max="60"
                             TabIndex="2"
                            onKeypress="return isNumber(event)"
                            AutoComplete="off"
                            runat="server"
                            placeholder=" " />
                        <asp:Label
                            CssClass="txtlabel"
                            runat="server">
                        Grace Period <span class="reqiredstar">*</span>
                        </asp:Label>
                    </div>
                    <asp:RequiredFieldValidator
                        ID="RfvtxtGracePeriod"
                        ValidationGroup="ShiftMstr"
                        ControlToValidate="txtGracePeriod"
                        runat="server"
                        CssClass="rfvStyle"
                        ErrorMessage="Enter Grace Period">
                    </asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row">
                <%-- Shift Start Time --%>
                <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                    <div class="txtboxdiv">
                        <asp:TextBox
                            ID="txtShiftStartTime"
                            CssClass="txtbox timePicker"
                             TabIndex="3"
                            runat="server"
                            placeholder=" " />
                        <asp:Label
                            CssClass="txtlabel"
                            runat="server">
                        Shift Start Time <span class="reqiredstar">*</span>
                        </asp:Label>
                    </div>
                    <asp:RequiredFieldValidator
                        ID="RfvtxtShiftStartTime"
                        ValidationGroup="ShiftMstr"
                        ControlToValidate="txtShiftStartTime"
                        runat="server"
                        CssClass="rfvStyle"
                        ErrorMessage="Enter Shift Start Time">
                    </asp:RequiredFieldValidator>
                </div>
                <%-- Shift End Time --%>
                <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                    <div class="txtboxdiv">
                        <asp:TextBox
                            ID="txtShiftEndTime"
                             TabIndex="4"
                            CssClass="txtbox timePicker"
                            runat="server"
                            placeholder=" " />
                        <asp:Label
                            CssClass="txtlabel"
                            runat="server">
                        Shift End Time <span class="reqiredstar">*</span>
                        </asp:Label>
                    </div>
                    <asp:RequiredFieldValidator
                        ID="RfvtxtShiftEndTime"
                        ValidationGroup="ShiftMstr"
                        ControlToValidate="txtShiftEndTime"
                        runat="server"
                        CssClass="rfvStyle"
                        ErrorMessage="Enter Shift End Time">
                    </asp:RequiredFieldValidator>
                </div>
                <%-- Break Start Time --%>
                <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                    <div class="txtboxdiv">
                        <asp:TextBox
                            ID="txtBreakStartTime"
                            CssClass="txtbox timePicker"
                            runat="server"
                             TabIndex="5"
                            placeholder=" " />
                        <asp:Label
                            CssClass="txtlabel"
                            runat="server">
                        Break Start Time <span class="reqiredstar">*</span>
                        </asp:Label>
                        <asp:RequiredFieldValidator
                            ID="rfvtxtBreakStartTime"
                            ValidationGroup="ShiftMstr"
                            ControlToValidate="txtBreakStartTime"
                            runat="server"
                            CssClass="rfvStyle"
                            ErrorMessage="Enter Break Start Time">
                        </asp:RequiredFieldValidator>
                    </div>
                </div>
                <%-- Break End Time --%>
                <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                    <div class="txtboxdiv">
                        <asp:TextBox
                            ID="txtBreakEndTime"
                            CssClass="txtbox timePicker"
                            runat="server"
                             TabIndex="6"
                            placeholder=" " />
                        <asp:Label
                            CssClass="txtlabel"
                            runat="server">
                        Break End Time <span class="reqiredstar">*</span>
                        </asp:Label>
                        <asp:RequiredFieldValidator
                            ID="rfvtxtBreakEndTime"
                            ValidationGroup="ShiftMstr"
                            ControlToValidate="txtBreakEndTime"
                            runat="server"
                            CssClass="rfvStyle"
                            ErrorMessage="Enter Break End Time">
                        </asp:RequiredFieldValidator>
                    </div>
                </div>
            </div>
            <%-- Buttons --%>
            <div class="float-end">
                <%-- Submit --%>
                <asp:Button
                    ID="BtnSubmit"
                    CssClass="btnSubmit"
                    runat="server"
                     TabIndex="7"
                    OnClick="BtnSubmit_Click"
                    ValidationGroup="ShiftMstr"
                    Text="Submit" />
                <%-- Cancel --%>
                <asp:Button
                    ID="BtnCancel"
                     TabIndex="8"
                    CssClass="btnCancel"
                    runat="server"
                    OnClick="BtnCancel_Click"
                    Text="Cancel" />
            </div>
        </div>

        <%-- GridView Side--%>
        <div id="DivGv" runat="server">
            <%-- Add Button --%>
            <div class="row">
                <div class="PageHeader">
                    <h4>Shift <span>Master</span></h4>

                    <div class="float-end">
                        <asp:LinkButton
                            ID="BtnAdd"
                            runat="server"
                            OnClick="BtnAdd_Click"
                            CssClass="btnAdd">
                          <i class="fa fa-plus AddPlus"></i>  Add</asp:LinkButton>
                    </div>
                </div>
            </div>

            <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 table-responsive">
                <%-- GridView --%>
                <asp:GridView ID="GvShiftMaster"
                    runat="server"
                    DataKeyNames="ShiftId,branchId,ActiveStatus"
                    AutoGenerateColumns="false"
                    CssClass="table table-striped table-hover border display gvFilter">
                    <Columns>
                        <%-- Sno --%>
                        <asp:TemplateField HeaderText="Sno">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <%-- Shift Name --%>
                        <asp:TemplateField
                            HeaderText="Shift Name"
                            HeaderStyle-HorizontalAlign="Center"
                            HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label
                                    ID="GvLblShiftName"
                                    runat="server"
                                    Text='<%#Bind("ShiftName") %>'>
                                </asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <%-- Shift StartTime --%>
                        <asp:TemplateField
                            HeaderText="Shift StartTime"
                            HeaderStyle-HorizontalAlign="Center"
                            HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label
                                    ID="GvLblStartTime"
                                    runat="server"
                                    Text='<%#Bind("StartTime") %>'>
                                </asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <%-- Shift EndTime --%>
                        <asp:TemplateField
                            HeaderText="Shift EndTime"
                            HeaderStyle-HorizontalAlign="Center"
                            HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label
                                    ID="GvLblEndTime"
                                    runat="server"
                                    Text='<%#Bind("EndTime") %>'>
                                </asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <%-- BreakStartTime --%>
                        <asp:TemplateField
                            HeaderText="Break StartTime"
                            HeaderStyle-HorizontalAlign="Center"
                            HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label
                                    ID="GvLblBreakStartTime"
                                    runat="server"
                                    Text='<%#Bind("BreakStartTime") %>'>
                                </asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <%-- BreakEndTime --%>
                        <asp:TemplateField
                            HeaderText="Break EndTime"
                            HeaderStyle-HorizontalAlign="Center"
                            HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label
                                    ID="GvLblBreakEndTime"
                                    runat="server"
                                    Text='<%#Bind("BreakEndTime") %>'>
                                </asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <%-- GracePeriod --%>
                        <asp:TemplateField
                            HeaderText="Grace Period"
                            HeaderStyle-HorizontalAlign="Center"
                            HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label
                                    ID="GvLblGracePeriod"
                                    runat="server"
                                    Text='<%#Bind("GracePeriod") %>'>
                                </asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <%-- Edit --%>
                        <asp:TemplateField HeaderText="Edit" HeaderStyle-CssClass="gvHeader">
                            <ItemTemplate>
                                <asp:ImageButton
                                    ID="ImgEdit"
                                    runat="server"
                                    src="../../img/edit-icon.png" alt="image" Width="25"
                                    OnClick="ImgEdit_Click"
                                    Visible='<%#Eval("ActiveStatus").ToString() =="A"?true:false%>' />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <%-- ActiveStatus --%>
                        <asp:TemplateField HeaderText="Delete" HeaderStyle-CssClass="gvHeader">
                            <ItemTemplate>
                                <asp:LinkButton
                                    ID="lnkActiveOrInactive"
                                    runat="server"
                                    CssClass='<%#Eval("ActiveStatus").ToString() =="A"?"gridActive":"gridDeActive"%>'
                                    OnClick="lnkActiveOrInactive_Click"
                                    Text='<%#Eval("ActiveStatus").ToString() =="A"?"Active":"Inactive"%>'></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>

