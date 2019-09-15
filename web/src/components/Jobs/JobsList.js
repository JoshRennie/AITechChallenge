import React from "react";
import Job from "./Job";
import { Row, Col } from "reactstrap";

export default function JobsList(props) {
  debugger;
  if (!props.jobs) {
    return <h4>Loading...</h4>;
  } else {
    return (
      <Row className="jobs-table">
        <Col>
          {props.jobs.reverse().map(job => {
            return <Job key={job.id} job={job} />;
          })}
        </Col>
      </Row>
    );
  }
}
