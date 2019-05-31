// appAuth.js

// buttons and event listeners
var btnSearch = document.getElementById('btnSearch');
var productDiv = document.getElementById('products');
var loadingSpinner = document.getElementById('loading');

btnSearch.addEventListener('click', function (e) {
    e.preventDefault();
    getProducts();
});

var getProducts = function () {
    loadingSpinner.style.display = 'block';
    var searchText = document.getElementById('txtSearch').value;
    var url = API_HOST + '?page=1&pageSize=50&searchText=' + searchText;
    return fetch(url,
        {
            headers: {
                'Authorization': 'Bearer ' + localStorage.getItem('accessToken')
            },
            method: 'GET'
        })
        .then(res => res.json())
        .then(function (result) {
            productDiv.innerHTML = "";
            var table = '<table class="table table-striped"><thead><tr><th>#</th><th>Model</th><th>Brand</th><th>Description</th></tr></thead><tbody>';
            for (var i = 0; i < result.data.length; i++) {
                table += '<tr>';
                table += '<th scope="row">' + (i + 1) + '</th>';
                table += '<td>' + result.data[i].model + '</td>';
                table += '<td>' + result.data[i].brand + '</td>';
                table += '<td>' + result.data[i].description + '</td>';
                table += '</tr>';
            }
            table += "</tbody></table>";
            productDiv.innerHTML = table;
            loadingSpinner.style.display = 'none';
        });
}