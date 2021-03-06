define(['knockout', 'postman', 'jquery'], function (ko, postman, $) { // needed fro require modules
    return function (params) { // needed for knockout components
        let names = ko.observableArray();
        let urls = ko.observableArray();
        let message = ko.observable();
        let selectedName = ko.observable();
        let currentPage = 1;
        let prevPage = 0;
        let nextPage = 2;
        var nextPageNav = function(){
            currentPage = nextPage;
            nextPage++;
            prevPage++;
            console.log("Get to next page");
            //location.reload();
        };
        
        var prevPageNav = function(){
            currentPage = prevPage;
            nextPage--;
            prevPage--;
            console.log("Get to prev page");
            //location.reload();
        };
        

        
        var makecall = $.ajax({
                url: 'http://localhost:5000/api/tag?pageNumber='+currentPage+'&pageSize=5',
                type: "GET",
                datatype: "json",
                processData:false,
                contentType: "application/json; charset=utf-8",
                success: function (res){
                    for(var i = 0; i < 5; i++)
                    {
                        var elem = {
                            title: res.result[i].tagName,
                            url: res.result[i].url,
                            postCount: res.result[i].postCount
                        };
                        
                        names.push(elem);
                        //urls.push(res.result[i].url);
                    }
                    //names(res.result[0].title);
                    console.log("Success!!");
                    
                    var chart = new CanvasJS.Chart("chartContainer", {
                            title:{
                                text: "Top 5 Tags"              
                            },
                            data: [              
                            {
                                // Change type to "doughnut", "line", "splineArea", etc.
                                type: "column",
                                dataPoints: [
                                    { label: names()[0].title,  y: names()[0].postCount  },
                                    { label: names()[1].title, y: names()[1].postCount  },
                                    { label: names()[2].title, y: names()[2].postCount  },
                                    { label: names()[3].title,  y: names()[3].postCount  },
                                    { label: names()[4].title,  y: names()[4].postCount  }
                                ]
                            }
                            ]
                    });
                    chart.render();
                },
                failure: function (result){
                    console.log("Error retrieving");
                }
        });
        

        
        
        
        let selectName = function (element) {
            console.log(element);
            selectedName(element);
        }
        
        let isSelected = function(name) {
            return name === selectedName();
        };
        postman.subscribe("someEvent", function (data) {
            message(data.msg);
        });

        return {
            names,
            message,
            selectName,
            selectedName,
            isSelected,
            urls,
            nextPageNav,
            prevPageNav
        };
    };
});