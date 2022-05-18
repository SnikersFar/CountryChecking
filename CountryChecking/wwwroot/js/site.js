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
            console.log(adresses);
            let bodyTable = $(".bodyOfTable");
            bodyTable.html('');
            if (adresses == null) {
                console.log("IS NULL");
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
            console.log(adresses[0]);
            console.log("CONTRY: " + adresses[0].country);
            console.log("LENGTH: " + adresses.length);
            if (adresses.length > 1) {
                window.location.hash = "zatemnenie";
                saveAdresses = adresses;
                for (let i = 0; i < adresses.length; i++) {
                    console.log(i);
                    let tr = $("<tr>");

                    tr.append('<th scope="row">' + (i + 1) + '</th>');
                    tr.click(function () {
                        trColor($(this));
                    });
                    let tdCountry = $("<td>");
                    tdCountry.html(adresses[i].country);
                    tr.append(tdCountry);

                    let tdDistrict = $("<td>");
                    tdDistrict.html(adresses[i].district);
                    tr.append(tdDistrict);

                    let tdCity = $("<td>");
                    tdCity.html(adresses[i].city);
                    tr.append(tdCity);

                    let tdPostlCode = $("<td>");
                    tdPostlCode.html(adresses[i].postalCode);
                    tr.append(tdPostlCode);

                    let tdStreet = $("<td>");
                    tdStreet.html(adresses[i].street);
                    tr.append(tdStreet);

                    let tdHouseNum = $("<td>");
                    tdHouseNum.text(adresses[i].houseNumber);
                    tr.append(tdHouseNum);

                    bodyTable.append(tr);
                }

            }
            else {
                console.log("A Lot off");
                //$(".CountrySelect").val(adresses[0].country);
                $(".City").val(adresses[0].city);
                $(".Street").val(adresses[0].street);
                $(".District").val(adresses[0].district);
                $(".Zip").val(adresses[0].postalCode);
                $(".HouseNumber").val(adresses[0].housenumber);

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
    $(".chooseButton").click(function () {
        console.log("CHOOSE-BUTTON");
        let MyTh = $(ChooseInfo).find("th").html();
        console.log(MyTh);
        let chooseAdress = saveAdresses[MyTh - 1];

        //$(".CountrySelect").val(chooseAdress.country);
        $(".City").val(chooseAdress.city);
        $(".Street").val(chooseAdress.street);
        $(".District").val(chooseAdress.district);
        $(".Zip").val(chooseAdress.postalCode);
        $(".HouseNumber").val(chooseAdress.houseNumber);


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
        window.location.hash = "";
    });
});

function trColor(tr) {
    console.log("TR");
    $(ChooseInfo).css("background-color", "white");
    ChooseInfo = tr;
    $(tr).css("background-color", "gray");
    console.log(ChooseInfo);
    //let MysTh = tr.

    console.log("MYTH ID = " + $(ChooseInfo).find("th").html());

}
