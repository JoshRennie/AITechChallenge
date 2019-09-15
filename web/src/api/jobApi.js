const baseUrl = process.env.REACT_APP_API_URL + "Jobs/";
const username = process.env.REACT_APP_API_USERNAME;
const password = process.env.REACT_APP_API_PASSWORD;

export function getList() {
  return callApi(baseUrl + "GetList").then(jobs => {
    return jobs.json();
  });
}

export function create() {
  return callApi(baseUrl + "Create", "POST").then(job => {
    return job.json();
  });
}

function callApi(url, method = "GET") {
  let headers = new Headers();
  headers.append(
    "Authorization",
    "Basic " + new Buffer(username + ":" + password).toString("base64")
  );

  return fetch(url, { headers: headers, method }).then(response => {
    return response;
  });
}
