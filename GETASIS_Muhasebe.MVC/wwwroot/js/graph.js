document.addEventListener("DOMContentLoaded", function () {
   
    var e = document.getElementById("MonthId");
    var value = e.value;
    var chart;
    var response;
 
    getData();
   
    async function getData() {
  
        response = await fetch('/Default/DataJson?year=' + value);
  

        var data = await response.json();
        length = data.length;
        var personelgelir = Array(12).fill(0);
        var personelgider = Array(12).fill(0);
        var carigelir = Array(12).fill(0);
        var carigider = Array(12).fill(0);
        var sahsigelir = Array(12).fill(0);
        var sahsigider = Array(12).fill(0);
        var digergelir = Array(12).fill(0);
        var digergider = Array(12).fill(0);
        var verigelir = Array(12).fill(0);
        var vergigider = Array(12).fill(0);
        for (i = 0; i < length; i++) {
            var month = data[i].month - 1;
            var gelir = data[i].income;
            var gider = data[i].outgoing;
            if (data[i].tip === "Personel") {
             
                personelgelir[month] = gelir;
                personelgider[month] = gider;
            }
            if (data[i].tip === "Diğer") {

                digergelir[month] = gelir;
                digergider[month] = gider;
            }
            if (data[i].tip === "Şahsi") {

                sahsigelir[month] = gelir;
                sahsigider[month] = gider;
            }
            if (data[i].tip === "Vergi") {

                verigelir[month] = gelir;
                vergigider[month] = gider;

            }
            if (data[i].tip === "Cari") {
                carigelir[month] = gelir;
                carigider[month] = gider;
            }

        }
   
      chart=   new Chart(document.getElementById("bar-chart"), {
            type: 'bar',
            //gelir dataset
            data: {
                labels: ["Ocak", "Şubat", "Mart", "Nisan", "Mayıs", "Haziran", "Temmuz", "Ağustos", "Eylül", "Ekim", "Kasım", "Aralık"],
                datasets: [
                  
                    {
                        label: 'Diğer Gelir',
                        backgroundColor: ["#7AFF33"],
                        data: digergelir,
                        stack: 'Stack 0',
                    },


                    {
                        label: 'Diğer Gider',
                        backgroundColor: ["#95FF5C"],
                        data: digergider,
                        stack: "stack 1",
                    },
                    {
                        label: 'Personel Gelir',
                        backgroundColor: ["#696CFF"],
                        data: personelgelir,
                        stack: 'Stack 0',
                    },
                    {
                        label: 'Personel Gider',
                        backgroundColor: ["#9698FF"],
                        data: personelgider,
                        stack: "stack 1",
                    },
                    {
                        label: 'Şahsi Gelir',
                        backgroundColor: ["#FF3E1D"],
                        data: sahsigelir,
                        stack: 'Stack 0',
                    },
                    {
                        label: 'Şahsi Gider',
                        backgroundColor: ["#FF674D"],
                        data: sahsigider,
                        stack: "stack 1",
                    },
                    {
                        label: 'Vergi Gelir',
                       
                        backgroundColor: ["#03C3EC"],
                        data: verigelir,
                        stack: 'Stack 0',
                    },
                    {
                        label: 'Vergi Gider',
                  
                        backgroundColor: ["#4ED5F2"],
                        data: vergigider,
                        stack: "stack 1",
                    },
                    {
                        label: 'Cari Gelir',
                        backgroundColor: ["#FFAC02"],
                        data: carigelir,
                        stack: 'Stack 0',
                    },
                    {
                        label: 'Cari Gider',
                        backgroundColor: ["#FFCD67"],
                        data: carigider,
                        stack: "stack 1",
                    },
                 




                ]
                ,


            },
            options: {
                plugins: {
                    title: {
                        display: false,
                        
                    },
                },
                responsive: true,
                interaction: {
                    intersect: true,
                },
                scales: {
                    x: {
                        stacked: true,
                    },
                   
                }
            }

        });

        

    }
    // update when select change
    $("#MonthId").change(function () {
        value = $(this).children("option:selected").val();
        getData();
        chart.destroy();
        chart.update();
    });
 

});


