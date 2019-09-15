import * as types from "../actions/actionTypes";

export default function jobReducer(state = [], action) {
  switch (action.type) {
    /* If we're creating a job, then add the new job to state */
    case types.CREATE_JOB:
      return [...state, { ...action.job }];
    /* If we're loading jobs, then update state to the new list of jobs */
    case types.LOAD_JOBS:
      return action.jobs;
    /* If we're deleting a job, then just remove it from state */
    case types.DELETE_JOB:
      return state.filter(job => job.Id !== action.id);
    default:
      return state;
  }
}
