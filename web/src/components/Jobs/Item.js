import React from "react";
import { Row, Col } from "reactstrap";

export default function Item({ item }) {
  return (
    <Row>
      <Col>
        <p>{item.itemName + " - " + item.quantity}</p>
      </Col>
    </Row>
  );
}
