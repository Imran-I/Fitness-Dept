<%@ Page Title="Message Templates" Language="C#" MasterPageFile="~/FitnessMstr.master" AutoEventWireup="true" CodeFile="MessageTemplates.aspx.cs" Inherits="Master_MessageTemplates" %>

<asp:Content ID="CntMessageTemplates" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="PageRoute">
        <div>
            <asp:Label ID="lblMainPage" CssClass="pageRoutecol" runat="server" Text="Home"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="lblNavFirst" runat="server" CssClass="pageRoutecol" Text="Other Setup"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="lblNavSecond" runat="server" CssClass="pageRoutecol" Text="Message Templates"></asp:Label>
        </div>
    </div>
    <div class="container-fluid frmcontainer">
        <div id="DivForm" runat="server" visible="false">
            <div class="PageHeader">
                <h5>Message  <span>Templates</span></h5>
            </div>

            <div class="row">
                <div class="col-12 col-sm-3 col-md-5 col-lg-3 col-xl-3 mb-3">
                    <div>
                        <asp:Label
                            CssClass="lblstyle"
                            runat="server">Template Type<span class="reqiredstar">*</span>
                        </asp:Label>
                    </div>
                    <asp:RadioButtonList ID="rbtntemplateType" runat="server" CssClass="frmcheckbox"
                        RepeatDirection="Horizontal" OnSelectedIndexChanged="rbtntemplateType_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Value="M" Selected="True">Mail</asp:ListItem>
                        <asp:ListItem Value="S">SMS</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <div runat="server" id="divPeid" class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                    <div class="txtboxdiv">
                        <asp:TextBox ID="txtpeid" CssClass="txtbox" Autocomplete="off" runat="server" placeholder=" " />
                        <asp:Label CssClass="txtlabel" runat="server">Pe Id<span class="reqiredstar">*</span></asp:Label>
                    </div>
                    <asp:RequiredFieldValidator ID="rfvpeid" ValidationGroup="MsgTemp" ControlToValidate="txtpeid" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Enter Peid">
                    </asp:RequiredFieldValidator>
                </div>
                <div runat="server" id="divTpid" class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                    <div class="txtboxdiv">
                        <asp:TextBox ID="txttpid" CssClass="txtbox" runat="server" Autocomplete="off" placeholder=" " />
                        <asp:Label CssClass="txtlabel" runat="server">Tp Id<span class="reqiredstar">*</span></asp:Label>
                    </div>
                    <asp:RequiredFieldValidator ID="rfvTpid" ValidationGroup="MsgTemp" ControlToValidate="txttpid" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Enter Tpid">
                    </asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row">
                <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                    <div class="txtboxdiv">
                        <asp:TextBox ID="txtmessageHeader" CssClass="txtbox" Autocomplete="off" runat="server" placeholder=" " />
                        <asp:Label CssClass="txtlabel" runat="server">Message Header <span class="reqiredstar">*</span></asp:Label>
                    </div>
                    <asp:RequiredFieldValidator ID="RfvmessageHeader" ValidationGroup="MsgTemp" ControlToValidate="txtmessageHeader" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Enter Message Header">
                    </asp:RequiredFieldValidator>
                </div>
                <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 mb-3">
                    <div class="txtboxdiv">
                        <asp:TextBox ID="txtsubject" CssClass="txtbox" runat="server" Autocomplete="off" placeholder=" " />
                        <asp:Label CssClass="txtlabel" runat="server">Subject <span class="reqiredstar">*</span></asp:Label>
                    </div>
                    <asp:RequiredFieldValidator ID="Rfvsubject" ValidationGroup="MsgTemp" ControlToValidate="txtsubject" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Enter Subject">
                    </asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="row">
                <div class="col-12 col-sm-10 col-md-10 col-lg-10 col-xl-10 mb-3">
                    <div class="txtboxdiv">
                        <asp:TextBox ID="txtmessageBody" TextMode="MultiLine" CssClass="txtbox" Autocomplete="off" runat="server" placeholder=" " />
                        <asp:Label CssClass="txtlabel" runat="server">Message Body <span class="reqiredstar">*</span></asp:Label>
                    </div>
                    <asp:RequiredFieldValidator ID="RfvmessageBody" ValidationGroup="MsgTemp" ControlToValidate="txtmessageBody" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Enter Message Body">
                    </asp:RequiredFieldValidator>
                </div>

            </div>

            <div class="float-end">
                <asp:Button ID="btnSubmit" CssClass="btnSubmit" ValidationGroup="MsgTemp" runat="server" OnClick="btnSubmit_Click" Text="Submit" />
                <asp:Button ID="btnCancel" CssClass="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
            </div>
        </div>
        <div id="divGv" runat="server">
            <div class="row">
                <div class="PageHeader">
                    <h4>Message <span>Templates</span></h4>
                    <div class="float-end">
                        <asp:LinkButton ID="btnAdd" runat="server" OnClick="btnAdd_Click"
                            CssClass="btnAdd">   <i class="fa fa-plus AddPlus"></i>  Add</asp:LinkButton>
                    </div>

                </div>

            </div>

            <div id="divGridView" runat="server" class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 table-responsive">
                <asp:GridView ID="gvMessageTemplate" runat="server" DataKeyNames="uniqueId" AutoGenerateColumns="false"
                    CssClass="table table-striped table-hover border display gvFilter">
                    <Columns>
                        <asp:TemplateField HeaderText="Sno">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="uniqueId" Visible="false" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lbluniqueId" runat="server" Text='<%#Bind("uniqueId") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Message Header" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblmessageHeader" runat="server" Text='<%#Bind("messageHeader") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Subject" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblsubject" runat="server" Text='<%#Bind("subject") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Message Body" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblmessageBody" runat="server" Text='<%#Bind("messageBody") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="templateType" Visible="false" HeaderStyle-CssClass="gvHeader">
                            <ItemTemplate>
                                <asp:Label ID="lbltemplateType" runat="server" Text='<%#Bind("templateType") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="peid" Visible="false" HeaderStyle-CssClass="gvHeader">
                            <ItemTemplate>
                                <asp:Label ID="lblpeid" runat="server" Text='<%#Bind("peid") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="tpid" Visible="false" HeaderStyle-CssClass="gvHeader">
                            <ItemTemplate>
                                <asp:Label ID="lbltpid" runat="server" Text='<%#Bind("tpid") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Edit" HeaderStyle-CssClass="gvHeader">
                            <ItemTemplate>
                                <asp:ImageButton
                                    ID="LnkEdit"
                                    runat="server"
                                    src="../../img/edit-icon.png" alt="image" Width="25"
                                    Text="Edit"
                                     OnClick="LnkEdit_Click" />
                            </ItemTemplate>

                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>

