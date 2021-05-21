$(document).ready(function () {
    var templatePersona = $('#hidden-template-persona').html();

    //Add an extra parameter in Agent Section
    $("#addExtraAttribute").click(function () {
        $('.js-persona-attribs-panel').append(templatePersona);
    });

    //Remove the last extra parameter in Agent Section
    $("#removeExtraAttribute").click(function () {
        $('.js-extra-attributes-persona-template:last-child').remove();
    });


    //Submit form to /Preferences/opt-down
    $('#submitPersona').click(function (evt) {
        var querystring = [];

        var firstName = $('#FirstName').val();
        var lastName = $('#LastName').val();
        var phone = $('#Phone').val();
        var emailAddress = $('#EmailAddress').val();
        

        if (firstName.trim() !== '')
            querystring.push({ name: 'FirstName', value: firstName.trim() });

        if (lastName.trim() !== '')
            querystring.push({ name: 'LastName', value: lastName.trim() });

        if (phone.trim() !== '')
            querystring.push({ name: 'Phone', value: phone.trim() });

        if (emailAddress.trim() !== '')
            querystring.push({ name: 'EmailAddress', value: emailAddress.trim() });

        $('.js-extra-attributes-persona-template').each(function (index, element) {
            var parameterName = $(element).find('input[name="key"]').val();
            var parameterValue = $(element).find('input[name="value"]').val();

            if (parameterName !== '' || parameterValue !== '') {
                querystring.push({ name: parameterName, value: parameterValue });
            }
        });

        var flagBase64Enabled = ($('#flagBase64Enabled').val() == 'true');
        var url;

        if (flagBase64Enabled)
            url = '/Home/GetPersona?params=' + btoa($.param(querystring));
        else
            url = '/Home/GetPersona?' + $.param(querystring);

        window.open(url, '_blank');

        return false;
    });

});
