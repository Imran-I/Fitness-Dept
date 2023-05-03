<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Fitness</title>
    <link rel="icon" href="img/FitnessLogo.png">
    <!-- Single Login popup Styles -->
    <link href="Css/SingleLogin/SingleLogin.css" rel="stylesheet" />
    <%--Bootstrap 5.0.2 CDN--%>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC" crossorigin="anonymous">
    <%--Nav CSS--%>
    <link href="Css/NavBar.css" rel="stylesheet" />
    <%--Content Page Css--%>
    <link href="Css/ContentPage.css" rel="stylesheet" />
    <!-- Icon Font Stylesheet -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.10.0/css/all.min.css" rel="stylesheet">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.2.1/css/all.min.css" integrity="sha512-MV7K8+y+gLIBoVD59lQIYicR65iaqukzvf/nwasF0nqhPay5w/9lJmVM2hMDcnK1OnMGCdVK+iQrJ7lzPJQd1w==" crossorigin="anonymous" referrerpolicy="no-referrer" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.4.1/font/bootstrap-icons.css" rel="stylesheet">

    <style>
        #google-button {
            margin-top: 0px;
        }

        .container {
            margin: 20px;
            background-color: rgba(66, 133, 244, 0.15);
            padding: 10px;
            border-radius: 10px;
            width: 450px;
            margin-top: 25px;
            display: none;
        }

        img {
            width: 56px;
            /*border-radius: 50%;*/
        }

        .id,
        .email,
        .name {
            display: inline-block;
            font-family: 'Verdana';
        }

        .name {
            font-size: 30px;
            position: relative;
            top: -16px;
            margin-left: 5px;
        }

        lable {
            font-family: 'Arial Black';
        }

        button {
            display: block;
            background-color: #4285F4;
            border: 0px;
            padding: 8px 20px;
            color: white;
            margin-top: 15px;
            cursor: pointer;
            outline: none;
        }

        .btnResnd {
            width: auto;
            font-size: 12px;
            font-weight: bold;
            padding: 12px 12px;
            letter-spacing: 1px;
            text-transform: uppercase;
            transition: transform 80ms ease-in;
            border-radius: 10px;
        }

        .rfvColor {
            color: red;
        }
    </style>
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>

    <script lang="javascript" type="text/javascript">

        function successalert(sMsg) {
            swal({
                title: 'Fitness',
                text: sMsg,
                icon: "success"
            });
        }
        function infoalert(sMsg) {
            swal({
                title: 'Fitness',
                text: sMsg,
                icon: "info"
            });
        }
        function erroralert(sMsg) {
            swal({
                title: 'Fitness',
                text: sMsg,
                icon: "error"
            });
        }
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }


        function SendOtp() {
            let seconds = 31;
            let button = document.querySelector('#<%=btnResend.ClientID%>');
            let buttonConfrm = document.getElementById('<%=btnCfmOtp.ClientID %>');
            //document.getElementById('txtMobileNo').value = hfMobileNo.Value;
            function incrementSeconds() {

                seconds = seconds - 1;
                if (seconds < 10) {
                    button.value = '00:0' + seconds;
                    button.disabled = true;
                }
                else {
                    button.value = '00:' + seconds;
                    button.disabled = true;
                }
                if (seconds == 0) {
                    seconds = 31;
                    buttonConfrm.style.display = 'none';
                    document.getElementById('<%=hfOtp.ClientID %>').value = "";
                    button.value = "ReSend OTP";
                    clearInterval(cancel);
                    button.disabled = false;
                }
            }
            var cancel = setInterval(incrementSeconds, 1000);
        }




    </script>
    <!-- Customized Bootstrap Stylesheet -->
    <link href="Css/bootstrap.min.css" rel="stylesheet" />
    <!-- Login Stylesheet -->
    <link href="Css/Login.css" rel="stylesheet" />

</head>
<body>
    <form id="form1" runat="server">
        <div id="SingleLogin_Overlay"
            class="SingleLogin_Overlay d-none"
            onclick="javascript:__doPostBack('LnkbtnSingleLoginClose','')"
            runat="server">
        </div>
        <div id="SingleLogin_Popup" class="SingleLogin_Popup d-none" runat="server">
            <div class="PopUp_Close">
                <div>
                    <a
                        onclick="javascript:__doPostBack('LnkbtnSingleLoginClose','')"
                        class="fa-solid fa-xmark fa-close"></a>
                </div>
            </div>
            <div class="Popup_Info">
                <svg
                    width="60px"
                    height="60px"
                    viewBox="0 0 60 60"
                    xmlns="http://www.w3.org/2000/svg"
                    fill="#212529"
                    class="bi bi-exclamation-square-fill">
                    <path
                        d="M7.5 0a7.5 7.5 0 0 0 -7.5 7.5v45a7.5 7.5 0 0 0 7.5 7.5h45a7.5 7.5 0 0 0 7.5 -7.5V7.5a7.5 7.5 0 0 0 -7.5 -7.5H7.5zm22.5 15c2.006 0 3.577 1.733 3.375 3.731l-1.313 13.151a2.07 2.07 0 0 1 -4.125 0L26.625 18.731A3.394 3.394 0 0 1 30 15zm0.007 22.5a3.75 3.75 0 1 1 0 7.5 3.75 3.75 0 0 1 0 -7.5z" />
                </svg>
            </div>
            <div class="Popup_InfoMessage">
                <div class="Message_Head">
                    Your session is currently active in another window/browser/system.
                </div>
                <div class="Message_Body">
                    Click <strong class="MessageBody_Ok">Ok</strong> to close your previous session and login to new session.
                </div>
            </div>
            <div class="PopUp_Ok">
                <input type="button" onclick="javascript:__doPostBack('btnSingleLoginPopUpOk','')" value="Ok" class="btn btn-dark btnOk" />
            </div>
        </div>

        <asp:LinkButton
            ID="LnkbtnSingleLoginClose"
            runat="server"
            ClientIDMode="Static"
            OnClick="LnkbtnSingleLoginClose_Click"
            CssClass="d-none">
        </asp:LinkButton>
        <asp:Button
            ID="btnSingleLoginPopUpOk"
            OnClick="btnSingleLoginPopUpOk_Click"
            ClientIDMode="Static"
            runat="server"
            CssClass="d-none"
            Text="ok" />


        <div class="Logincontent">
            <div class="row LoginContainer">
                <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 LoginHeight">
                    <img src="img/FitnessLogo.png" class="LoginLogoImage" />
                    <div class="LoginDatas">
                        <p class="LoginHeader">Create an Account</p>
                        <p class="LoginSubHeader">Let's Get Started</p>

                        <div class="txtboxdiv">
                            <asp:TextBox ID="txtMobileNo" MaxLength="10" onkeydown="return (event.keyCode!=13)" MinLength="10" AutoComplete="off"
                                onKeyPress="return isNumber(event)" CssClass="txtbox" runat="server" placeholder=" " />
                            <asp:Label ID="lblMobile" CssClass="txtlabel" runat="server">Mobile No. <span class="reqiredstar">*</span></asp:Label>
                        </div>
                        <asp:RequiredFieldValidator ID="RfvtxtMobileNo" runat="server"
                            ControlToValidate="txtMobileNo" ErrorMessage="Enter MobileNo" CssClass="rfvColor"></asp:RequiredFieldValidator>

                        <div class="txtboxdiv">
                            <asp:TextBox ID="txtotp" onkeydown="return (event.keyCode!=13)" AutoComplete="off" CssClass="txtbox" Visible="false" runat="server" placeholder=" " />
                            <asp:Label CssClass="txtlabel" ID="lblOtp" Visible="false" runat="server">OTP <span class="reqiredstar">*</span></asp:Label>
                        </div>

                        <div class="txtboxdiv">
                            <asp:TextBox ID="txtMail" AutoPostBack="true" OnTextChanged="txtMail_TextChanged" MaxLength="10" MinLength="10" AutoComplete="off" CssClass="txtbox d-none" runat="server" />
                            <asp:Label ID="lblMail" CssClass="txtlabel d-none" runat="server">Mail Id. <span class="reqiredstar">*</span></asp:Label>
                        </div>
                        <div class="LoginButton">
                            <asp:Button ID="btnSendOTP" CssClass="btnSubmit d-none" runat="server" OnClick="btnSendOTP_Click" Text="Send OTP" />
                            <asp:Button ID="btnCancel" CssClass="btnCancel d-none" runat="server" Text="Cancel" />
                        </div>
                        <div class="LoginResendButton">
                            <asp:Button ID="btnCfmOtp" runat="server" CssClass="btnVerify" Visible="false" ValidationGroup="OTP"
                                Text="Verify & Login" OnClick="btnCfmOtp_Click" />
                            <%--  <asp:Button ID="btnverify" CssClass="btnSubmit d-none"   runat="server" OnClick="btnSendOTP_Click" Text="Send OTP" />--%>
                            <asp:Button ID="btnResend" runat="server" class="btnResnd" Visible="false"
                                OnClick="btnResend_Click" />
                        </div>


                        <div class="LoginGmail">
                            <p class="LoginGmailHeader">or Login / SignUp </p>
                            <div class="divSingUpOption">
                                <div class="row socialmedia">
                                    <div id="google-button" class="imgText">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 LoginRight">
                    <img class="LoginMocImage" src="img/LoginMoc.png">
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hfOtp" runat="server" />
        <asp:HiddenField ID="hfMobileNo" runat="server" />
    </form>


    <%--Bootstrap Js--%>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/js/bootstrap.bundle.min.js" integrity="sha384-MrcW6ZMFYlzcLA8Nl+NtUVF0sA7MsXsP1UyJoMp4YLEuNSfAP+JcXn/tWtIaxVXM" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.2/dist/umd/popper.min.js" integrity="sha384-IQsoLXl5PILFhosVNubq5LC7Qb9DXgDA9i+tQ8Zj3iwWAwPtgFTxbJ8NT4GN1R8p" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/js/bootstrap.min.js" integrity="sha384-cVKIPhGWiC2Al4u+LWgxfKTRIcfu0JTxR+EQDz/bgldoEyl4H0zUF0QKbrJ0EcQF" crossorigin="anonymous"></script>

    <script src="https://code.jquery.com/jquery-3.4.1.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.0/dist/js/bootstrap.bundle.min.js"></script>

    <script>



        var googleButton = document.getElementById('google-button');

        // function to get response
        function handleCredentialResponse(response) {
            var responsePayload = decodeJwtResponse(response.credential);
            document.getElementById('txtMail').classList.remove('d-none');
            document.getElementById('lblMail').classList.remove('d-none');
            document.getElementById('txtMobileNo').classList.add('d-none');
            document.getElementById('lblMobile').classList.add('d-none');
            document.getElementById('txtMail').value = responsePayload.email;
            googleButton.style.display = 'none';
            //document.getElementById("txtMobileNo").click();
            $(document.getElementById('txtMail')).change();
        }

        window.onload = function () {
            google.accounts.id.initialize({
                /*client_id: "351957529447-531uvcccddpt51cu76k18dji3spg2tne.apps.googleusercontent.com",*/
                client_id: "135455860385-caqagcqpt8tbgp74e6rphncd3if3gi5r.apps.googleusercontent.com",
                callback: handleCredentialResponse,
                auto_select: true,
                auto: true
            });
            google.accounts.id.renderButton(
                document.getElementById("google-button"),
                { theme: 'filled_black', size: "medium", width: '200', shape: "pill" }
            );
            google.accounts.id.prompt();
        }

        // function to decode the response.credential
        function decodeJwtResponse(token) {
            var base64Url = token.split('.')[1];
            var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
            var jsonPayload = decodeURIComponent(atob(base64).split('').map(function (c) {
                return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
            }).join(''));
            return JSON.parse(jsonPayload);
        }

        $(document.getElementById('txtMobileNo')).keyup(function () {
            var count = document.getElementById('txtMobileNo').value
            if (count.length == 10) {
                document.getElementById('btnSendOTP').classList.remove('d-none');
                document.getElementById('btnCancel').classList.remove('d-none');
                document.getElementById('txtMobileNo').readOnly = true;
            }
        });


        $(document.getElementById('btnCancel')).click(function () {
            document.getElementById('txtMobileNo').readOnly = false;
            document.getElementById('txtMobileNo').value = "";
            document.getElementById('txtMail').value = "";
            document.getElementById('btnSendOTP').classList.add('d-none');
            document.getElementById('btnCancel').classList.add('d-none');
        });
    </script>
    <script src="https://accounts.google.com/gsi/client" async defer></script>
</body>
</html>
