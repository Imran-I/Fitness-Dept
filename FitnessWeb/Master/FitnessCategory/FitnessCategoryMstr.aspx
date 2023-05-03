<%@ Page Title="Category Master" Language="C#" MasterPageFile="~/FitnessMstr.master" AutoEventWireup="true" CodeFile="FitnessCategoryMstr.aspx.cs" Inherits="Master_Configuration_FitnessCategoryMstr" %>

<asp:Content ID="CtFitnessCategory" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
            color: #202124;
            font-size: 1rem;
        }


        .imgpreview {
            width: 100px;
            height: 100px;
            margin-bottom: 0.2rem;
            border-radius: 4rem;
            margin-left: 1rem;
            border: 1px solid;
        }
    </style>

    <div class="PageRoute">
        <div>
            <asp:Label ID="lblMainPage" CssClass="pageRoutecol" runat="server" Text="Home"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="lblNavFirst" runat="server" CssClass="pageRoutecol" Text="Gym Setup"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="lblNavSecond" runat="server" CssClass="pageRoutecol" Text="Fitness Plan Setup"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="lblNavThird" runat="server" CssClass="pageRoutecol" Text="Category Master"></asp:Label>
        </div>
    </div>
    <div class="container-fluid frmcontainer">
        <div id="DivForm" runat="server" visible="false">
            <div class="PageHeader">
                <h5>Category <span>Master</span></h5>
            </div>

            <div class="ddl">
                <div class="row">
                    <div class="col-12 col-sm-9 col-md-9 col-lg-9 col-xl-9 mb-9">
                        <div class="row">

                            <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 mb-3">
                                <div class="txtboxdiv">
                                    <asp:TextBox ID="txtCategoryName" runat="server" CssClass="txtbox"  TabIndex="1" AutoComplete="off" MaxLength="50" placeholder=" " />
                                    <asp:Label CssClass="txtlabel" runat="server">Category Name<span class="reqiredstar">*</span></asp:Label>
                                    <asp:RequiredFieldValidator ID="rfdCategoryName" ValidationGroup="CategoryMstr"
                                        ControlToValidate="txtCategoryName" runat="server" CssClass="rfvStyle"
                                        ErrorMessage="Enter Category Name">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 mb-3">
                                <div class="txtboxdiv">
                                    <asp:TextBox ID="txtDescription" TextMode="MultiLine" runat="server" TabIndex="2" AutoComplete="off" 
                                        MaxLength="200" CssClass="txtbox" placeholder=" " />
                                    <asp:Label CssClass="txtlabel" runat="server">Description<span class="reqiredstar">*</span></asp:Label>
                                    <asp:RequiredFieldValidator ID="rfdShortName" ValidationGroup="CategoryMstr"
                                        ControlToValidate="txtDescription" runat="server" CssClass="rfvStyle"
                                        ErrorMessage="Enter Description">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                        <img id="imgpreview" clientidmode="Static" runat="server" class="imgpreview" src="../../img/Defaultupload.png" />
                        <asp:FileUpload ID="fuimage" CssClass="mx-4" TabIndex="3" runat="server" onchange="showpreview(this);" />
                    </div>
                </div>
            </div>
            <div class="float-end">
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" TabIndex="4" OnClick="btnSubmit_Click"
                    CssClass="btnSubmit" ValidationGroup="CategoryMstr" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="5" CssClass="btnCancel" OnClick="btnCancel_Click" />
            </div>
        </div>

        <div id="divGv" runat="server">
            <div class="row">
                <div class="PageHeader">
                    <h4>Category <span>Master</span></h4>

                    <div class="float-end">
                        <asp:LinkButton ID="btnAdd" runat="server" OnClick="btnAdd_Click"
                            CssClass="btnAdd">
                             <i class="fa fa-plus AddPlus"></i>  Add</asp:LinkButton>
                    </div>

                </div>

            </div>

            <div id="divGridView" runat="server" class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 table-responsive">
                <asp:GridView ID="gvCategory" runat="server" DataKeyNames="categoryId" AutoGenerateColumns="false"
                    CssClass="table table-striped table-hover border display gvFilter">
                    <Columns>
                        <asp:TemplateField HeaderText="Sno">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Category Id" Visible="false" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblcategoryId" runat="server" Text='<%#Bind("categoryId") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="GymOwnerId" Visible="false" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblgymOwnerId" runat="server" Text='<%#Bind("gymOwnerId") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="BranchId" Visible="false" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblbranchId" runat="server" Text='<%#Bind("branchId") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Category Name" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblcategoryName" runat="server" Text='<%#Bind("categoryName") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Description" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lbldescription" runat="server" Text='<%#Bind("description") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="imageUrl" Visible="false" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lblimageUrl" runat="server" Text='<%#Bind("imageUrl") %>'></asp:Label>
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

                        <asp:TemplateField HeaderText="Add Benefits" HeaderStyle-CssClass="gvHeader">
                            <ItemTemplate>
                                <asp:LinkButton ID="linkAddDetails"
                                    CssClass="GridAddBtn" Visible='<%#Eval("activeStatus").ToString() =="A"?true:false%>' OnClick="linkAddDetails_Click" runat="server">Add</asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

        </div>
    </div>

    <%--CategoryBenifits--%>
    <div id="DivSubForm" runat="server" class="DisplyCard" visible="false">
        <div class="CtgryDisplyCardPostion table-responsive">
            <div class="PageHeader">
                <h5>Category <span>Benefits</span>
                    <a onclick="btnClose()" class="float-end btnclose">
                        <i class="fa-solid fa-x"></i></a>
                </h5>
                <div class="text-start">
                    <a class="addlblHead">Category Name :
                        <asp:Label ID="lblCategoryName" runat="server"></asp:Label></a>
                </div>
            </div>
            <div class="row">
                <div class="col-12 col-sm-9 col-md-9 col-lg-9 col-xl-9 mb-9">
                    <div class="row">
                        <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                            <asp:DropDownList ID="ddlType" TabIndex="1" CssClass="form-select" runat="server">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" InitialValue="0" ValidationGroup="SubBenefits"
                                ControlToValidate="ddlType" runat="server" CssClass="rfvStyle"
                                ErrorMessage="Select Type">
                            </asp:RequiredFieldValidator>
                        </div>
                        <div class="col-12 col-sm-8 col-md-8 col-lg-8 col-xl-8 mb-3">
                            <div class="txtboxdiv">
                                <asp:TextBox ID="txtSubDescription" TabIndex="2" TextMode="MultiLine" MaxLength="150" CssClass="txtbox" runat="server" placeholder=" " />
                                <asp:Label CssClass="txtlabel" runat="server">Description <span class="reqiredstar">*</span></asp:Label>
                                <asp:RequiredFieldValidator ID="RfvDescription" ValidationGroup="SubBenefits" ControlToValidate="txtSubDescription"
                                    runat="server" CssClass="rfvStyle" ErrorMessage="Enter Description">
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                    <%--                    <div class="profile">Image</div>--%>
                    <asp:Image ID="imgBeneFitsPhotoPrev" class="imgpreview" ClientIDMode="Static" runat="server" ImageUrl="~/img/Defaultupload.png" />
                    <asp:FileUpload ID="FuSubimage" CssClass="mx-4" TabIndex="3" runat="server" onchange="ShowImagePreview(this);" />
                </div>
            </div>
            <div class="text-end">
                <asp:Button CssClass="btnSubmit" ID="btnSubSubmit" ValidationGroup="SubBenefits" OnClick="btnSubSubmit_Click" runat="server" Text="Submit" />
                <asp:Button ID="btnSubCancel" CssClass="btnCancel" runat="server" Text="Cancel" OnClick="btnSubCancel_Click" />
            </div>
            <hr />
            <div id="div3" runat="server" class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 table-responsive" style="margin-bottom: 0px !important">
                <asp:GridView ID="gvCatBenefitsMstr" Style="font-size: 0.8rem" runat="server" DataKeyNames="uniqueId" AutoGenerateColumns="false"
                    CssClass="table table-striped table-hover border" >
                    <Columns>
                        <asp:TemplateField HeaderText="Sno">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Category" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label ID="lbluniqueId" runat="server" Text='<%#Bind("uniqueId") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblcategoryId" runat="server" Text='<%#Bind("categoryId") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblcategoryName" runat="server" Text='<%#Bind("categoryName") %>' Visible="true"></asp:Label>

                                <asp:Label ID="lblimageUrl" runat="server" Text='<%#Bind("imageUrl") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblactiveStatus" runat="server" Text='<%#Bind("activeStatus") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Benefit Type" HeaderStyle-CssClass="gvHeader">
                            <ItemTemplate>
                                <asp:Label ID="lbltype" runat="server" Text='<%#Bind("type") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblBenefitName" runat="server" Text='<%#Bind("BenefitName") %>' Visible="true"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Description" HeaderStyle-CssClass="gvHeader">
                            <ItemTemplate>
                                <asp:Label ID="lbldescription" runat="server" Text='<%#Bind("description") %>' Visible="true"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Edit" HeaderStyle-CssClass="gvHeader">
                            <ItemTemplate>
                                <asp:ImageButton
                                    ID="LnkSubEdit"
                                    runat="server"
                                    src="../../img/edit-icon.png" alt="image" Width="25"
                                    Text="Edit"
                                    Visible='<%#Eval("activeStatus").ToString() =="A"?true:false%>' OnClick="LnkSubEdit_Click" />
                            </ItemTemplate>

                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Delete" HeaderStyle-CssClass="gvHeader">
                            <ItemTemplate>
                                <asp:LinkButton
                                    ID="lnkActiveOrInactive"
                                    runat="server"
                                    CssClass='<%#Eval("activeStatus").ToString() =="A"?"gridActive":"gridDeActive"%>'
                                    Text='<%#Eval("activeStatus").ToString() =="A"?"Active":"Inactive"%>' OnClick="lnkActiveOrInactiveSub_Click"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function ShowImagePreview(input) {
            var fup = document.getElementById("<%=FuSubimage.ClientID %>");
            var fileName = fup.value;
            var maxfilesize = 1024 * 1024;
            filesize = input.files[0].size;
            var ext = fileName.substring(fileName.lastIndexOf('.') + 1);
            if (ext == "gif" || ext == "GIF" || ext == "PNG" || ext == "png" || ext == "jpg" || ext == "JPG" || ext == "bmp" || ext == "BMP" || ext == "jpeg" || ext == "JPEG") {
                if (filesize <= maxfilesize) {
                    if (input.files && input.files[0]) {
                        var reader = new FileReader();
                        reader.onload = function (e) {
                            $('#<%=imgBeneFitsPhotoPrev.ClientID%>').prop('src', e.target.result);

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


    <script type="text/javascript">
        function showpreview(input) {
            debugger
            //if (input.files && input.files[0]) {

            //    var reader = new FileReader();
            //    reader.onload = function (e) {
            //        $('#imgpreview').attr('src', e.target.result);
            //    }
            //    reader.readAsDataURL(input.files[0]);
            //}
            var fup = document.getElementById("<%=fuimage.ClientID %>");
            var fileName = fup.value;
            var maxfilesize = 1024 * 1024;
            filesize = input.files[0].size;
            var ext = fileName.substring(fileName.lastIndexOf('.') + 1);
            if (ext == "gif" || ext == "GIF" || ext == "PNG" || ext == "png" || ext == "jpg" || ext == "JPG" || ext == "bmp" || ext == "BMP" || ext == "jpeg" || ext == "JPEG") {
                if (filesize <= maxfilesize) {
                    if (input.files && input.files[0]) {
                        var reader = new FileReader();
                        reader.onload = function (e) {
                            $('#<%=imgpreview.ClientID%>').prop('src', e.target.result);

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

        function btnClose() {
            $('#<%= DivSubForm.ClientID %>').css("display", "none");
        }
    </script>
</asp:Content>

