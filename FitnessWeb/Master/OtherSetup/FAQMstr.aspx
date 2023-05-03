<%@ Page Title="FAQ Master" Language="C#" MasterPageFile="~/FitnessMstr.master" EnableEventValidation="false"
    AutoEventWireup="true" CodeFile="FAQMstr.aspx.cs" Inherits="Master_FAQMstr" %>

<asp:Content ID="CntFAQMstr" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="PageRoute">
        <div>
            <asp:Label ID="lblMainPage" CssClass="pageRoutecol" runat="server" Text="Home"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="lblNavFirst" runat="server" CssClass="pageRoutecol" Text="Other Setup"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="lblNavSecond" runat="server" CssClass="pageRoutecol" Text="FAQ Master"></asp:Label>
        </div>
    </div>
    <div class="container-fluid frmcontainer">
        <div id="DivForm" runat="server" visible="false">
            <div class="PageHeader">
                <h5>FAQ <span>Master</span></h5>
            </div>
            <div class="row">
                <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3" runat="server">
                    <asp:DropDownList ID="ddlQuestionType" CssClass="form-select" AutoPostBack="true" TabIndex="1"
                        OnSelectedIndexChanged="ddlQuestionType_SelectedIndexChanged" runat="server">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RfvQuestionType" InitialValue="0" ValidationGroup="FAQMstr" ControlToValidate="ddlQuestionType" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Select Question Type">
                    </asp:RequiredFieldValidator>
                </div>
                <div id="divOffer" class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3" runat="server" visible="false">
                    <asp:DropDownList ID="ddlOfferList" TabIndex="2" CssClass="form-select" runat="server">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RfvOffer" InitialValue="0" ValidationGroup="FAQMstr" ControlToValidate="ddlOfferList" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Select Offer">
                    </asp:RequiredFieldValidator>
                </div>
                <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                    <div class="txtboxdiv">
                        <asp:TextBox ID="txtQuestion" TabIndex="3"  Autocomplete="off" CssClass="txtbox" runat="server" placeholder=" " />
                        <asp:Label CssClass="txtlabel" runat="server">Question <span class="reqiredstar">*</span></asp:Label>
                    </div>
                    <asp:RequiredFieldValidator ID="Rfvquestion" ValidationGroup="FAQMstr" ControlToValidate="txtQuestion" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Enter Question">
                    </asp:RequiredFieldValidator>
                </div>
                <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                    <div class="txtboxdiv">
                        <asp:TextBox ID="txtAnswer" TabIndex="4" CssClass="txtbox"  Autocomplete="off" runat="server" placeholder=" " />
                        <asp:Label CssClass="txtlabel" runat="server">Answer <span class="reqiredstar">*</span></asp:Label>
                    </div>
                    <asp:RequiredFieldValidator ID="Rfvanswer" ValidationGroup="FAQMstr" ControlToValidate="txtAnswer" runat="server" CssClass="rfvStyle"
                        ErrorMessage="Enter Answer">
                    </asp:RequiredFieldValidator>
                </div>

            </div>
            <div class="float-end">
                <asp:Button ID="btnSubmit" TabIndex="5" CssClass="btnSubmit" ValidationGroup="FAQMstr" runat="server" OnClick="btnSubmit_Click" Text="Submit" />
                <asp:Button ID="btnCancel" TabIndex="6" CssClass="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
            </div>

        </div>

        <div id="divGv" runat="server">
            <div class="row">
                <div class="PageHeader">
                    <h4>FAQ <span>Master</span></h4>

                    <div class="float-end">
                        <asp:LinkButton ID="btnAdd" runat="server" OnClick="btnAdd_Click"
                            CssClass="btnAdd">   <i class="fa fa-plus AddPlus"></i>  Add</asp:LinkButton>
                    </div>

                </div>

            </div>

            <div id="divGridView" runat="server" class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 table-responsive">
                <asp:GridView ID="gvFAQ" runat="server" DataKeyNames="faqId" AutoGenerateColumns="false"
                    CssClass="table table-striped table-hover border display gvFilter">
                    <Columns>
                        <asp:TemplateField HeaderText="Sno">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="faqId" Visible="false" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblfaqId" runat="server" Text='<%#Bind("faqId") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="offerId" Visible="false" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblofferId" runat="server" Text='<%#Bind("offerId") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Question" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblquestion" runat="server" Text='<%#Bind("question") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Answer" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblanswer" runat="server" Text='<%#Bind("answer") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="questionType" Visible="false" HeaderStyle-CssClass="gvHeader">
                             <ItemTemplate>
                                 <asp:Label ID="lblquestionType" runat="server" Text='<%#Bind("questionType") %>'></asp:Label>
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
                                    Visible='<%#Eval("activeStatus").ToString() =="A"?true:false%>' OnClick="LnkEdit_Click" />
                            </ItemTemplate>

                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Delete" HeaderStyle-CssClass="gvHeader">
                            <ItemTemplate>
                                <asp:LinkButton
                                    ID="lnkActiveOrInactive"
                                    runat="server"
                                    CssClass='<%#Eval("activeStatus").ToString() =="A"?"gridActive":"gridDeActive"%>'
                                    Text='<%#Eval("activeStatus").ToString() =="A"?"Active":"Inactive"%>' OnClick="lnkActiveOrInactive_Click"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>

</asp:Content>

