var ChooseInfo;
var saveAdresses;
$(document).ready(function () {


    $(".submitButton").click(function (event) {
        console.log("123");
        event.preventDefault();
        $.post('/Home/GetAdress',
            {
                Country: $(".CountrySelect").val(),
                City: $(".City").val(),
                Street: $(".Street").val(),
                District: $(".District").val(),
                Zip: $(".Zip").val(),
                HouseNumber: $(".HouseNumber").val(),
            }
        ).done(function (adresses) {
            let bodyTable = $(".bodyOfTable");
            bodyTable.html('');
            if (adresses.length > 1) {
                window.location.hash = "zatemnenie";
                saveAdresses = adresses;
                for (let i = 0; i < adresses.length; i++) {
                    console.log(i);
                    let tr = $("<tr>");

                    tr.append('<th scope="row">' + (i + 1) + '</th>');
                    tr.click(function () {
                        trColor(this);
                    });
                    let tdCountry = $("<td>");
                    tdCountry.text = adresses[i].Country;
                    tr.append(tdCountry);

                    let tdDistrict = $("<td>");
                    tdDistrict.text = adresses[i].District;
                    tr.append(tdDistrict);

                    let tdCity = $("<td>");
                    tdCity.text = adresses[i].City;
                    tr.append(tdCity);

                    let tdPostlCode = $("<td>");
                    tdPostlCode.text = adresses[i].PostalCode;
                    tr.append(tdPostlCode);

                    let tdStreet = $("<td>");
                    tdStreet.text = adresses[i].Street;
                    tr.append(tdStreet);

                    let tdHouseNum = $("<td>");
                    tdHouseNum.text = adresses[i].HouseNumber;
                    tr.append(tdHouseNum);

                    bodyTable.append(tr);
                }

            }
            else {
                $.get('/Home/GetOutput',
                    {
                        Country: $(".CountrySelect").val(),
                        City: $(".City").val(),
                        Street: $(".Street").val(),
                        District: $(".District").val(),
                        Zip: $(".Zip").val(),
                        HouseNumber: $(".HouseNumber").val(),
                    }
                ).done(function (outInfo) {
                    $(".Output").val(outInfo);
                });
            }

        });


    });
    $(".CancelChoose").click(function () {
        window.location.hash = "";
    });
    $(".ChooseButton").click(function () {
        let chooseAdress = saveAdresses[$(ChooseInfo).first($("th")).val() - 1];

        $(".CountrySelect").val(chooseAdress.Country);
        $(".City").val(chooseAdress.City);
        $(".Street").val(chooseAdress.Street);
        $(".District").val(chooseAdress.District),
            $(".Zip").val(chooseAdress.PostalCode);
        $(".HouseNumber").val(chooseAdress.HouseNumber);


        $.get('/Home/GetOutput',
            {
                Country: $(".CountrySelect").val(),
                City: $(".City").val(),
                Street: $(".Street").val(),
                District: $(".District").val(),
                Zip: $(".Zip").val(),
                HouseNumber: $(".HouseNumber").val(),
            }
        ).done(function (outInfo) {
            $(".Output").val(outInfo);
        });
    });
});

function trColor(tr) {
    console.log("TR");
    $(ChooseInfo).css("background-color", "white");
    ChooseInfo = tr;
    $(tr).css("background-color", "gray");
}
