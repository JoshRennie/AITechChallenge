import * as types from "./actionTypes";
import * as jobApi from "../../api/jobApi";

function loadJobsSuccess(jobs) {
  return { type: types.LOAD_JOBS, jobs };
}

function createJobSuccess(job) {
  return { type: types.CREATE_JOB, job };
}

function handleError(error) {
  alert(error);
}

export function loadJobs() {
  return function(dispatch) {
    return jobApi
      .getList()
      .then(jobs => {
        dispatch(loadJobsSuccess(jobs));
      })
      .catch(error => {
        handleError(error);
      });
  };
}

export function createJob() {
  return function(dispatch) {
    return jobApi
      .create()
      .then(job => {
        dispatch(createJobSuccess(job));
      })
      .catch(error => {
        handleError(error);
      });
  };
}
