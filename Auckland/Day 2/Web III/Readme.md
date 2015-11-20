# Continuous Deployment

If you have your projects on GitHub, you can set up continuous deployment so that your web app is automatically deployed when you commit to the main branch

To find out more, have a look through the section called 'Deploy files from a repository site like BitBucket, CodePlex, Dropbox, GitHub, or Mercurial' here- https://azure.microsoft.com/en-us/documentation/articles/web-sites-publish-source-control/

# Edge Tools

Quick site scan your websites, generate screenshots and more using Edge Tools - https://dev.windows.com/en-us/microsoft-edge/tools/

# Visual Studio Team Services + GitHub

To build GitHub hosted projects in VSTS (Visual Studio Team Services), follow the instructions here - https://msdn.microsoft.com/en-us/Library/vs/alm/Build/github/index

# Connecting your API

There are a few changes we need to make to make your API work with the frontend code:

1. In your ajax call in StudentModule.js, change the dataType of your ajax call from **jsonp** to **json**
2. In the same ajax call change the url being called to the url of *your* api
Example:
```javascript
  $.ajax({
      type: "GET",
      dataType: "json",
      url: "http://msauniversity.azurewebsites.net/api/Students",
      success: function (data) {
          callback(data);
      }
  });
```

3.) Change the property names being accessed in index.js when displaying back to screen
```javascript
    var lastnamecol = document.createElement('td');
    lastnamecol.innerHTML = students[i].LastName;
    row.appendChild(lastnamecol);

    var firstnamecol = document.createElement('td');
    firstnamecol.innerHTML = students[i].FirstMidName;
    row.appendChild(firstnamecol);

    var enrollmentdatecol = document.createElement('td');
    enrollmentdatecol.innerHTML = students[i].EnrollmentDate;
    row.appendChild(enrollmentdatecol);
```

4.) Change your**th** tags in **index.html** to match the new properties
```HTML
  <thead>
      <tr>
          <th>Last Name</th>
          <th>First Name</th>
      </tr>       
  </thead>
````
