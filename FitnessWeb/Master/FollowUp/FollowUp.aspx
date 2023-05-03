<%@ Page Title="FollowUp" Language="C#" MasterPageFile="~/FitnessMstr.master" AutoEventWireup="true" CodeFile="FollowUp.aspx.cs" Inherits="Master_FollowUp_FollowUp" %>

<asp:Content ID="CndFollowUp" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="PageRoute">
        <div>
            <asp:Label ID="Label1" CssClass="pageRoutecol" runat="server" Text="Home"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="Label2" runat="server" CssClass="pageRoutecol" Text="Enrollment"></asp:Label>
            <i class="fafaicon">/</i>
            <asp:Label ID="Label3" runat="server" CssClass="pageRoutecol" Text="Follow Up"></asp:Label>
        </div>

    </div>
    <div class="container-fluid frmcontainer">
        <div id="divMain" runat="server">
            <div class="PageHeader">
                <h5>Follow  <span>Up</span></h5>
            </div>
            <div class="row">
                <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3">
                    <div class="txtboxdiv">
                        <asp:TextBox ID="txtFromdate" TabIndex="1" CssClass="txtbox FromDate" runat="server" placeholder=" " />
                        <asp:Label CssClass="txtlabel" runat="server">From Date</asp:Label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ValidationGroup="Offer"
                            ControlToValidate="txtFromdate" runat="server" CssClass="rfvStyle"
                            ErrorMessage="Enter From Date">
                        </asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="col-12 col-sm-3 col-md-3 col-lg-3 col-xl-3 mb-3">
                    <div class="txtboxdiv">

                        <asp:TextBox ID="txtTodate" TabIndex="2" CssClass="txtbox ToDate" runat="server" placeholder=" " />
                        <asp:Label CssClass="txtlabel" runat="server">To Date</asp:Label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ValidationGroup="Offer"
                            ControlToValidate="txtTodate" runat="server" CssClass="rfvStyle"
                            ErrorMessage="Enter To Date">
                        </asp:RequiredFieldValidator>
                    </div>
                </div>
            </div>
            <div class="float-end mb-5">
                <asp:Button ID="btnSearch" OnClick="btnSearch_Click" TabIndex="3" ValidationGroup="Offer" CssClass="btnSubmit" runat="server" Text="🔎︎ Search" />
            </div>
            <br />
            <br />
            <asp:GridView ID="gvFollowUp" runat="server" AutoGenerateColumns="false"
                CssClass="table table-striped table-hover border display gvFilter" DataKeyNames="userId" OnRowDataBound="gvFollowUp_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="Sno">
                        <ItemTemplate>
                            <%#Container.DataItemIndex+1 %>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="User Name" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                        <ItemTemplate>
                            <asp:Label ID="lblgvuserIds" runat="server" Text='<%# Bind("userId") %>' Font-Bold="true" Visible="false" Width="100px"></asp:Label>
                            <asp:Label ID="lblUserName" runat="server" Text='<%#Bind("UserName") %>' Visible="true"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Training Type Name" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                        <ItemTemplate>
                            <asp:Label ID="lbltrainingTypeName" runat="server" Text='<%#Bind("trainingTypeName") %>' Visible="true"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Category Name" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle">
                        <ItemTemplate>
                            <asp:Label ID="lblcategoryName" runat="server" Text='<%#Bind("categoryName") %>' Visible="true"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Diet Time List" HeaderStyle-CssClass="gvHeader" Visible="false">
                        <ItemTemplate>
                            <asp:DataList runat="server" ID="dlFollowupDetails" RepeatDirection="Vertical">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvuserId" runat="server" Text='<%# Bind("userId") %>' Font-Bold="true" Visible="false" Width="100px"></asp:Label>
                                    <asp:Label ID="lblgvDateOfDay" runat="server" Text='<%# Bind("DateOfDay") %>' Font-Bold="true" Visible="false" Width="100px"></asp:Label>
                                    <asp:Label ID="lblgvDaysName" runat="server" Text='<%# Bind("DaysName") %>' Font-Bold="true" Visible="false" Width="100px"></asp:Label>
                                    <asp:Label ID="lblgvConsumedCalories" runat="server" Text='<%# Bind("ConsumedCalories") %>' Font-Bold="true" Visible="false" Width="100px"></asp:Label>
                                    <asp:Label ID="lblgvCompletedActivity" runat="server" Text='<%# Bind("CompletedActivity") %>' Font-Bold="true" Visible="false" Width="100px"></asp:Label>
                                </ItemTemplate>
                            </asp:DataList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="View">
                        <ItemTemplate>
                            <asp:LinkButton ID="LnkView" runat="server" OnClick="LnkView_Click">
                                    <i class="fa fa-eye" style="color:black"></i>
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <div id="DivView" runat="server" visible="false">
            <div class="PageHeader">
                <h5>Follow  <span>Details</span>
                    <asp:LinkButton ID="btnClose" runat="server" OnClick="btnClose_Click" class="float-end btnclose">
                        <i class="fa-solid fa-arrow-left"></i></asp:LinkButton>
                </h5>
                <div class="text-start">
                    <a class="addlblHead">User Name :
                        <asp:Label ID="lblUserames" runat="server"></asp:Label></a>
                </div>
            </div>
            <div id="DivGvViewUser" style="font-size: 0.8rem" runat="server" class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 table-responsive">
                <asp:GridView ID="gvUserView" Style="font-size: 0.8rem" runat="server" DataKeyNames="userId" AutoGenerateColumns="false"
                    CssClass="table table-striped table-hover border display gvFilter">
                    <Columns>
                        <asp:TemplateField HeaderText="Sno">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date" HeaderStyle-CssClass="gvHeader">
                            <ItemTemplate>
                                <asp:Label ID="lbluserId" runat="server" Text='<%#Bind("userId") %>' Visible="false"></asp:Label>
                                <asp:LinkButton ID="lblDate" runat="server" OnClick="lblDate_Click" Text='<%#Bind("DateOfDay") %>' Visible="true"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Days" HeaderStyle-CssClass="gvHeader">
                            <ItemTemplate>
                                <asp:Label ID="lblDaysName" runat="server" Text='<%#Bind("DaysName") %>' Visible="true"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Consumed Calories" HeaderStyle-CssClass="gvHeader">
                            <ItemTemplate>
                                <asp:Label ID="lblConsumedCalories" runat="server" Text='<%#Bind("ConsumedCalories") %>' Visible="true"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Completed Activity" HeaderStyle-CssClass="gvHeader">
                            <ItemTemplate>
                                <asp:Label ID="lblCompletedActivity" runat="server" Text='<%#Bind("CompletedActivity") %>' Visible="true"></asp:Label>
                                <asp:Label ID="lblDateOfDay" runat="server" Text='<%#Bind("DateOfDay") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <div id="DivGvFoodList" visible="false" style="font-size: 0.8rem" runat="server" class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 table-responsive">
                <div class="text-start">
                    <a class="addlblHead">Date:
                        <asp:Label ID="lblFDate" runat="server"></asp:Label></a>
                </div>
                <asp:GridView ID="GvFoodList" Style="font-size: 0.8rem" runat="server" DataKeyNames="uniqueId" AutoGenerateColumns="false"
                    CssClass="table table-striped table-hover border display gvFilter">
                    <Columns>
                        <asp:TemplateField HeaderText="Sno">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Food Name" HeaderStyle-CssClass="gvHeader">
                            <ItemTemplate>
                                <asp:Label ID="lbluniqueId" runat="server" Text='<%#Bind("uniqueId") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblfoodItemName" runat="server" Text='<%#Bind("foodItemName") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Calories" HeaderStyle-CssClass="gvHeader">
                            <ItemTemplate>
                                <asp:Label ID="lblcalories" runat="server" Text='<%#Bind("calories") %>' Visible="true"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Meal Type" HeaderStyle-CssClass="gvHeader">
                            <ItemTemplate>
                                <asp:Label ID="lblmealTypeName" runat="server" Text='<%#Bind("mealTypeName") %>' Visible="true"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Time" HeaderStyle-CssClass="gvHeader">
                            <ItemTemplate>
                                <asp:Label ID="lblfromTime" runat="server" Text='<%#Bind("fromTime") %>' Visible="true"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <div id="DivGvWorkoutList" visible="false" style="font-size: 0.8rem" runat="server" class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 table-responsive">
                <asp:GridView ID="GvWorkoutList" Style="font-size: 0.8rem" runat="server" DataKeyNames="uniqueId" AutoGenerateColumns="false"
                    CssClass="table table-striped table-hover border display gvFilter">
                    <Columns>
                        <asp:TemplateField HeaderText="Sno">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Workout Name" HeaderStyle-CssClass="gvHeader">
                            <ItemTemplate>
                                <asp:Label ID="lbluniqueId" runat="server" Text='<%#Bind("uniqueId") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblworkoutCatTypeName" runat="server" Text='<%#Bind("workoutCatTypeName") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Workout Type Name" HeaderStyle-CssClass="gvHeader">
                            <ItemTemplate>
                                <asp:Label ID="lblworkoutTypeName" runat="server" Text='<%#Bind("workoutTypeName") %>' Visible="true"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sets" HeaderStyle-CssClass="gvHeader">
                            <ItemTemplate>
                                <asp:Label ID="lblsetTypeName" runat="server" Text='<%#Bind("setTypeName") %>' Visible="true"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="No Of Reps" HeaderStyle-CssClass="gvHeader">
                            <ItemTemplate>
                                <asp:Label ID="lblnoOfReps" runat="server" Text='<%#Bind("noOfReps") %>' Visible="true"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Weight" HeaderStyle-CssClass="gvHeader">
                            <ItemTemplate>
                                <asp:Label ID="lblweight" runat="server" Text='<%#Eval("weight") + "Kgs" %>' Visible="true"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>

    </div>
    <asp:HiddenField ID="hfListClose" runat="server" />
    <script src="../../Js/CommonDate/CommonFromAndToDate.js"></script>
</asp:Content>

