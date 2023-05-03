<%@ Page Title="Diet Type" Language="C#" MasterPageFile="~/FitnessMstr.master" AutoEventWireup="true" CodeFile="DietTypeMstr.aspx.cs" Inherits="Master_DietTypeMstr" %>

<asp:Content ID="CtDietType" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
    </style>

    <div class="PageRoute">
        <div>
            <asp:Label ID="lblMainPage" CssClass="pageRoutecol" runat="server" Text="Home"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="lblNavFirst" runat="server" CssClass="pageRoutecol" Text="Common Setup"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="lblNavSecond" runat="server" CssClass="pageRoutecol" Text="Food Menu Setup"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="lblNavThird" runat="server" CssClass="pageRoutecol" Text="Diet Type Master"></asp:Label>

        </div>
    </div>
    <div class="container-fluid frmcontainer">
        <div id="DivForm" runat="server" visible="false">
            <div class="PageHeader">
                <h5>Diet Type <span>Master</span></h5>
            </div>

            <div class="ddl">
                <div class="row">
                    <div class="col-12 col-sm-9 col-md-9 col-lg-9 col-xl-9 mb-9">
                        <div class="row">
                            <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                                <asp:DropDownList ID="ddlDietType" TabIndex="1" CssClass="form-select" runat="server">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RfvDietType" ValidationGroup="DietTypeMstr"
                                    ControlToValidate="ddlDietType" runat="server" CssClass="rfvStyle"
                                    ErrorMessage="Select Diet Type" InitialValue="0">
                                </asp:RequiredFieldValidator>
                            </div>

                            <div class="col-12 col-sm-8 col-md-8 col-lg-8 col-xl-8 mb-3">
                                <div class="txtboxdiv">
                                    <asp:TextBox ID="txtDescription" TabIndex="2" AutoComplete="Off" runat="server" CssClass="txtbox"
                                       Maxlength="150" TextMode="MultiLine" placeholder=" " />
                                    <asp:Label CssClass="txtlabel" runat="server">Description<span class="reqiredstar">*</span></asp:Label>

                                    <asp:RequiredFieldValidator ID="RfvDescription" ValidationGroup="DietTypeMstr"
                                        ControlToValidate="txtDescription" runat="server" CssClass="rfvStyle"
                                        ErrorMessage="Enter Description">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>


                        </div>
                        <div class="row">
                            <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 mb-3">
                                <div class="txtboxdiv">
                                    <asp:TextBox ID="txtTypeIndicationUrl" TabIndex="3" AutoComplete="Off" runat="server" CssClass="txtbox"
                                        placeholder=" " TextMode="MultiLine" />
                                    <asp:Label CssClass="txtlabel" runat="server">Type Indication Url<span class="reqiredstar">*</span></asp:Label>
                                    <asp:RequiredFieldValidator ID="RfvTypeIndicationUrl" ValidationGroup="DietTypeMstr"
                                        ControlToValidate="txtTypeIndicationUrl" runat="server" CssClass="rfvStyle"
                                        ErrorMessage="Enter Type Indication Url">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                        <asp:Image ID="imgDietPhotoPrev" class="imgpreview" TabIndex="4" ClientIDMode="Static" runat="server" ImageUrl="../../img/Defaultupload.png" />
                        <asp:FileUpload ID="Fuimage" CssClass="mx-4" TabIndex="5" runat="server" onchange="ShowImagePreview(this);" />
                    </div>
                </div>
            </div>

            <div class="float-end">
                <asp:Button CssClass="btnSubmit" ID="btnSubmit" TabIndex="6" runat="server" Text="Submit" ValidationGroup="DietTypeMstr" OnClick="btnSubmit_Click" />
                <asp:Button ID="btnCancel" CssClass="btnCancel" TabIndex="7" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
            </div>
        </div>

        <div id="divGv" runat="server">
            <div class="row">
                <div class="PageHeader">
                    <h4>Diet Type <span>Master</span></h4>
                    <div class="float-end">
                        <asp:LinkButton ID="btnAdd" runat="server" OnClick="btnAdd_Click"
                            CssClass="btnAdd">
                            <i class="fa fa-plus AddPlus"></i>  Add</asp:LinkButton>
                    </div>
                </div>
            </div>

            <div id="divGridView" runat="server" class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 table-responsive">
                <asp:GridView ID="gvDietMstr" runat="server" DataKeyNames="dietTypeId" AutoGenerateColumns="false"
                    CssClass="table table-striped table-hover border display gvFilter">
                    <Columns>
                        <asp:TemplateField HeaderText="Sno">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Diet Type Name" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lbldietTypeId" runat="server" Text='<%#Bind("dietTypeId") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lbldietTypeNameId" runat="server" Text='<%#Bind("dietTypeNameId") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lbldietTypeName" runat="server" Text='<%#Bind("dietTypeName") %>' Visible="true"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Description" HeaderStyle-HorizontalAlign="Center" Visible="false" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lbldescription" runat="server" Text='<%#Bind("description") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ImageUrl" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblimageUrl" runat="server" Text='<%#Bind("imageUrl") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Type Indication ImageUrl" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lbltypeIndicationImageUrl" runat="server" Text='<%#Bind("typeIndicationImageUrl") %>' Visible="false"></asp:Label>
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
    <asp:HiddenField ID="hfImageUrl" runat="server" />
    <script type="text/javascript">
        function ShowImagePreview(input) {

            var fup = document.getElementById("<%=Fuimage.ClientID %>");
               var fileName = fup.value;
               var maxfilesize = 1024 * 1024;
               filesize = input.files[0].size;
               var ext = fileName.substring(fileName.lastIndexOf('.') + 1);
               if (ext == "gif" || ext == "GIF" || ext == "PNG" || ext == "png" || ext == "jpg" || ext == "JPG" || ext == "bmp" || ext == "BMP" || ext == "jpeg" || ext == "JPEG") {
                   if (filesize <= maxfilesize) {
                       if (input.files && input.files[0]) {
                           var reader = new FileReader();
                           reader.onload = function (e) {
                               $('#<%=imgDietPhotoPrev.ClientID%>').prop('src', e.target.result);

                        };
                        reader.readAsDataURL(input.files[0]);

                    }
                }
                else {
                    swal("Please, Upload image file less than or equal to 1 MB !!!");
                    fup.focus();
                    return false;
                }
            }
            else {
                swal("Please, Upload Gif, Jpg, Jpeg or Bmp Images only !!!");
                fup.focus();
                return false;
            }
        }

    </script>
</asp:Content>

