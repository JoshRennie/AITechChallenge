import React from "react";
import { Row, Col } from "reactstrap";
import "../../css/Jobs.css";
import Item from "./Item";

export default function Job({ job }) {
  debugger;
  let backgroundClass = "";

  switch (job.approvalStatus) {
    case 1:
      backgroundClass = "approved-job";
      break;
    case 2:
      backgroundClass = "referred-job";
      break;
    case 3:
      backgroundClass = "declined-job";
      break;
    default:
      break;
  }

  return (
    <Row className={backgroundClass + " jobs-row"}>
      <Col>
        <h2>{job.name}</h2>
        <h4>Price: £{job.price}</h4>
        <h4>Labour Hours: {job.labourHours}</h4>
        <h4>Notes:</h4>
        <p>{job.approvalMessage}</p>
        <h4>Items: </h4>
        {job.items.map(item => {
          return <Item key={item.id} item={item} />;
        })}
      </Col>
    </Row>
  );
}
