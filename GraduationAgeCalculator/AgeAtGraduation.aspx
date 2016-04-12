<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AgeAtGraduation.aspx.cs" Inherits="GraduationAgeCalculator_AgeAtGraduation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Age at Graduation</title>
    <link href="StyleSheet.css" rel="stylesheet"/>
    <script type="text/javascript" src="../scripts/jquery-2.2.3.min.js"></script>
    <script type="text/javascript">

        function calculateAge() {
            var bMonth = birthMonth.value, bDay = birthDay.value, bYear = birthYear.value, gMonth = graduationMonth.value, gDay = graduationDay.value, gYear = graduationYear.value;
            var age = parseInt(gYear) - parseInt(bYear);
            if (parseInt(bMonth) - parseInt(gMonth) >= 0 && parseInt(bDay) - parseInt(gDay) > 0) {
                age--;
            }
            document.getElementById("ageResult").innerHTML = "You will be " + age.toString() + " years old when you graduate. Sweet!!";
        };

        function getServerDate() {
            var s = " ";
            $.ajax({
                type: "POST",
                url: 'AgeAtGraduation.aspx/getDate',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (results) {
                    document.getElementById("currentDate").innerHTML = results.d;
                },
                error: function (err) {
                    alert(err.status + " - " + err.statusText);
                }
            })
        };
        $(document).ready(getServerDate);
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Image ID="banner" runat="server" ImageUrl="mybanner.png" ResizeMode="Fit" Width="100%" Height="100%" />
    <p id="currentDate">
    </p>
    <p>How old will you be when you graduate
        <br />Enter your dates:
    </p>
        <div id="BirthInfo">Birthdate:<br />
            Month:<input id="birthMonth" type="text"/><br />
            Day:<input id="birthDay" type="text"/><br />
            Year:<input id="birthYear" type="text"/>
        </div>
       <div id="GraduationInfo">Graduation date:<br />
            Month:<input id="graduationMonth" type="text"/><br />
            Day:<input id="graduationDay" type="text"/><br />
            Year:<input id="graduationYear" type="text"/><br />

       </div>
        <div id="ageArea">       
            <input id="getAge" type="button" value="Get age" onclick="calculateAge()" />
        </div>
        <br />
       <asp:Label ID="ageResult" runat="server" Text=""></asp:Label>
    </div>
    </form>
</body>
</html>
