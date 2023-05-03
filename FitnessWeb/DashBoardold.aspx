<%@ Page Title="" Language="C#" MasterPageFile="~/FitnessMstr.master" AutoEventWireup="true" CodeFile="DashBoardold.aspx.cs" Inherits="DashBoardold" %>

<asp:Content ID="FrmDashBoard" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- Dashboard Style --%>
    <link href="Css/DashBoard/DashBoardold.css" rel="stylesheet" />


    <div class="container-fluid frmcontainer">


        <%-- FromAndToDate And Count --%>
        <div id="divFromDateToDateAndCount" runat="server" class="BranchAdmin">
            <%-- Heading --%>
            <div class="PageHeader">
                <h5>Dashboard </h5>
            </div>

            <div class="divFromAndToDate_Container">
                <div class="row">
                    <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                        <div class="txtboxdiv">
                            <%-- FromDate --%>
                            <asp:TextBox
                                ID="txtFromDate"
                                CssClass="txtbox DashFromDate"
                                runat="server"
                                MaxLength="15"
                                AutoComplete="off"
                                placeholder=" " />
                            <asp:Label
                                CssClass="txtlabel"
                                runat="server">
                               From Date <span class="reqiredstar">*</span>
                            </asp:Label>
                        </div>
                        <asp:RequiredFieldValidator
                            ID="RfvFromDate"
                            ValidationGroup="Dashboard"
                            ControlToValidate="txtFromDate"
                            runat="server"
                            CssClass="rfvStyle"
                            ErrorMessage="Select From Date">
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                        <div class="txtboxdiv">
                            <%-- ToDate --%>
                            <asp:TextBox
                                ID="txtToDate"
                                CssClass="txtbox DashToDate"
                                runat="server"
                                MaxLength="15"
                                AutoComplete="off"
                                placeholder=" " />
                            <asp:Label
                                CssClass="txtlabel"
                                runat="server">
                               To Date <span class="reqiredstar">*</span>
                            </asp:Label>
                        </div>
                        <asp:RequiredFieldValidator
                            ID="RfvToDate"
                            ValidationGroup="Dashboard"
                            ControlToValidate="txtToDate"
                            runat="server"
                            CssClass="rfvStyle"
                            ErrorMessage="Select To Date">
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="col-12 col-sm-4 col-md-4 col-lg-4 col-xl-4 mb-3">
                        <%-- Buttons --%>
                        <div class="float-end">
                            <%-- Submit --%>
                            <asp:Button
                                ID="BtnSearch"
                                CssClass="btnSubmit"
                                OnClick="BtnSearch_Click"
                                runat="server"
                                ValidationGroup="Dashboard"
                                Text="Search" />
                            <%-- Cancel --%>
                            <asp:Button
                                ID="BtnReset"
                                CssClass="btnCancel"
                                OnClick="BtnReset_Click"
                                runat="server"
                                Text="Reset" />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3 Count_Container">
                        <div class="Icon">
                            <svg width="36px" height="36px" viewBox="0 0 36 36" xmlns="http://www.w3.org/2000/svg">
                                <g>
                                    <path fill="none" d="M0 0h36v36H0z" />
                                    <path d="M13.5 27H6v-12h7.5v12zm-3 -3v-6H9v6h1.5zm9 0V12h-1.5v12h1.5zm3 3h-7.5V9h7.5v18zm6 -3V6h-1.5v18h1.5zm3 3h-7.5V3h7.5v24zm1.5 6H4.5v-3h28.5v3z" />
                                </g></svg>
                        </div>
                        <div class="divCountAndDescription">
                            <asp:LinkButton
                                ID="LnkBtnBookingCount"
                                runat="server"
                                OnClick="LnkBtnBookingCount_Click"
                                CssClass="count">
                            </asp:LinkButton>
                            <p class="description">Bookings</p>
                        </div>
                    </div>

                    <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3 Count_Container">
                        <div class="Icon">
                            <svg width="36px" height="36px" viewBox="0 0 36 36" xmlns="http://www.w3.org/2000/svg">
                                <g>
                                    <path fill="none" d="M0 0h36v36H0z" />
                                    <path d="M8.183 22.5L1.5 27.75V4.5a1.5 1.5 0 0 1 1.5 -1.5h22.5a1.5 1.5 0 0 1 1.5 1.5v18H8.183zm-1.038 -3H24V6H4.5v15.577L7.145 19.5zM12 25.5h15.355L30 27.577V12h1.5a1.5 1.5 0 0 1 1.5 1.5v20.25L26.317 28.5H13.5a1.5 1.5 0 0 1 -1.5 -1.5v-1.5z" />
                                </g>
                            </svg>
                        </div>
                        <div class="divCountAndDescription">
                            <asp:LinkButton
                                ID="LnkBtnEnquiryCount"
                                runat="server"
                                OnClick="LnkBtnEnquiryCount_Click"
                                CssClass="count">
                            </asp:LinkButton>
                            <p class="description">Enquiries</p>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <%-- GoBack --%>
        <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3 divGoBack_Container">
            <asp:Button
                ID="BtnBactoFromAndToDatePage"
                CssClass="btnSubmit"
                OnClick="BtnBactoFromAndToDatePage_Click"
                runat="server"
                Text="Go Back" />
        </div>

        <%-- Booking List GridView Container --%>
        <div id="divBookingList" runat="server">
            <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 table-responsive">
                <%--BookingList Heading --%>
                <div class="PageHeader">
                    <h5>Booking List</h5>
                </div>
                <%--BookingList Grid View --%>
                <asp:GridView ID="GvBookingList"
                    runat="server"
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

                        <%-- bookingId --%>
                        <asp:TemplateField
                            HeaderText="BookingId"
                            HeaderStyle-HorizontalAlign="Center"
                            HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label
                                    ID="GvLblBookingId"
                                    runat="server"
                                    Text='<%#Bind("bookingId") %>'>
                                </asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>

                        <%-- booking Date --%>
                        <asp:TemplateField
                            HeaderText="Booking Date"
                            HeaderStyle-HorizontalAlign="Center"
                            HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label
                                    ID="GvLblbookingDate"
                                    runat="server"
                                    Text='<%#Bind("bookingDate") %>'>
                                </asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>

                        <%-- User Name --%>
                        <asp:TemplateField
                            HeaderText="User Name"
                            HeaderStyle-HorizontalAlign="Center"
                            HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label
                                    ID="GvLbluserName"
                                    runat="server"
                                    Text='<%#Bind("userName") %>'>
                                </asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>

                        <%-- From Date --%>
                        <asp:TemplateField
                            HeaderText="Valid From"
                            HeaderStyle-HorizontalAlign="Center"
                            HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label
                                    ID="GvLblValidfromDate"
                                    runat="server"
                                    Text='<%#Bind("fromDate") %>'>
                                </asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>

                        <%-- To Date --%>
                        <asp:TemplateField
                            HeaderText="Valid To"
                            HeaderStyle-HorizontalAlign="Center"
                            HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label
                                    ID="GvLblValidtoDate"
                                    runat="server"
                                    Text='<%#Bind("toDate") %>'>
                                </asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>

                        <%-- PhoneNumber --%>
                        <asp:TemplateField
                            HeaderText="Phone Number"
                            HeaderStyle-HorizontalAlign="Center"
                            HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label
                                    ID="GvLblphoneNumber"
                                    runat="server"
                                    Text='<%#Bind("phoneNumber") %>'>
                                </asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>

                        <%-- categoryName --%>
                        <asp:TemplateField
                            HeaderText="Category Name"
                            HeaderStyle-HorizontalAlign="Center"
                            HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label
                                    ID="GvLblcategoryName"
                                    runat="server"
                                    Text='<%#Bind("categoryName") %>'>
                                </asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>

                        <%-- trainingType --%>
                        <asp:TemplateField
                            HeaderText="Training Type"
                            HeaderStyle-HorizontalAlign="Center"
                            HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label
                                    ID="GvLbltrainingType"
                                    runat="server"
                                    Text='<%#Bind("trainingType") %>'>
                                </asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>

                        <%-- trainingMode --%>
                        <asp:TemplateField
                            HeaderText="Training Mode"
                            HeaderStyle-HorizontalAlign="Center"
                            HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label
                                    ID="GvLbltrainingMode"
                                    runat="server"
                                    Text='<%#Bind("trainingMode") %>'>
                                </asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>

                        <%-- totalAmount --%>
                        <asp:TemplateField
                            HeaderText="Total Amount"
                            HeaderStyle-HorizontalAlign="Center"
                            HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label
                                    ID="GvLbltotalAmount"
                                    runat="server"
                                    Text='<%#Bind("totalAmount") %>'>
                                </asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>

        <%-- Entries List GridView Container --%>
        <div id="divEnquiryList" runat="server">
            <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 table-responsive">
                <%--Enquiries Heading --%>
                <div class="PageHeader">
                    <h5>Enquiries List</h5>
                </div>
                <%--Enquiry List Grid View --%>
                <asp:GridView ID="GvEnquiriesList"
                    runat="server"
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

                        <%-- User Name --%>
                        <asp:TemplateField
                            HeaderText="User Name"
                            HeaderStyle-HorizontalAlign="Center"
                            HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label
                                    ID="GvLbluserName"
                                    runat="server"
                                    Text='<%#Bind("userName") %>'>
                                </asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>

                        <%-- age --%>
                        <asp:TemplateField
                            HeaderText="Age"
                            HeaderStyle-HorizontalAlign="Center"
                            HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label
                                    ID="GvLblage"
                                    runat="server"
                                    Text='<%#Bind("age") %>'>
                                </asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>

                        <%-- gender --%>
                        <asp:TemplateField
                            HeaderText="Gender"
                            HeaderStyle-HorizontalAlign="Center"
                            HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label
                                    ID="GvLblgender"
                                    runat="server"
                                    Text='<%#Bind("gender") %>'>
                                </asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>

                        <%-- PhoneNumber --%>
                        <asp:TemplateField
                            HeaderText="Phone Number"
                            HeaderStyle-HorizontalAlign="Center"
                            HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label
                                    ID="GvLblphoneNumber"
                                    runat="server"
                                    Text='<%#Bind("phoneNumber") %>'>
                                </asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>

                        <%-- enquiryDate  --%>
                        <asp:TemplateField
                            HeaderText="Enquiry Date"
                            HeaderStyle-HorizontalAlign="Center"
                            HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label
                                    ID="GvLblenquiryDate"
                                    runat="server"
                                    Text='<%#Bind("enquiryDate") %>'>
                                </asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>

                        <%-- enquiryReason  --%>
                        <asp:TemplateField
                            HeaderText="Enquiry Reason"
                            HeaderStyle-HorizontalAlign="Center"
                            HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label
                                    ID="GvLblenquiryReason"
                                    runat="server"
                                    Text='<%#Bind("enquiryReason") %>'>
                                </asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>

                        <%-- followUpStatusName --%>
                        <asp:TemplateField
                            HeaderText="Followup Status"
                            HeaderStyle-HorizontalAlign="Center"
                            HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label
                                    ID="GvLblfollowUpStatusName"
                                    runat="server"
                                    Text='<%#Bind("followUpStatusName") %>'>
                                </asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>

                        <%-- followUpModeName --%>
                        <asp:TemplateField
                            HeaderText="Followup Mode"
                            HeaderStyle-HorizontalAlign="Center"
                            HeaderStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:Label
                                    ID="GvLblfollowUpModeName"
                                    runat="server"
                                    Text='<%#Bind("followUpModeName") %>'>
                                </asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>

    <%-- Dashboar Fromdate and ToDate Sript --%>
    <script src="Js/Dashboard/Dashboard.js"></script>
</asp:Content>