(function () {
    $(function () {
        var domain = 'http://localhost:7410';



        console.log('***1here is my custom content!');
        var basicAuthUI =
            '<div class="input"><input placeholder="username" id="input_username" name="username" type="text" size="10"/></div>' +
            '<div class="input"><input placeholder="password" id="input_password" name="password" type="password" size="10"/></div>' +
            '<div class="input"><button id= "getToken" type="button">Get Token</button></div>';

        $(basicAuthUI).insertBefore('#api_selector div.input:last-child');
        $("#input_apiKey").hide();


        $("#getToken").on("click", function () {
            var username = $('#input_username').val();
            var password = $('#input_password').val();

            var loginData = {
                grant_type: 'password',
                username: username,
                password: password
            };

            $.ajax({
                type: 'POST',
                url: domain+ '/api/Token',
                data: loginData
            }).done(function (data) {
                debugger;
                var apiKeyAuth = new SwaggerClient.ApiKeyAuthorization("Authorization", "Bearer " + data.access_token, "header");
                window.swaggerUi.api.clientAuthorizations.add("bearer", apiKeyAuth);
                window.log("Set bearer token: " + data.access_token);

            });
        });

    });
})();