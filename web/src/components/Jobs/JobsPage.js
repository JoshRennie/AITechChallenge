import React, { useEffect } from "react";
import { connect } from "react-redux";
import { bindActionCreators } from "redux";
import * as jobActions from "../../redux/actions/jobActions";
import { Container, Row, Col } from "reactstrap";
import JobsList from "./JobsList";

function JobsPage(props) {
  useEffect(() => {
    props.actions.loadJobs();
  }, [props.actions]);

  function handleAddClick() {
    props.actions.createJob();
  }

  return (
    <Container fluid>
      <Row style={{ marginTop: 20 }}>
        <Col sm="3"></Col>
        <Col sm="6">
          <h2>Jobs</h2>
          <button
            onClick={() => handleAddClick()}
            style={{ marginBottom: 30 }}
            className="btn btn-primary"
          >
            Add Job
          </button>
          <JobsList jobs={[...props.jobs]} />
        </Col>
      </Row>
    </Container>
  );
}

function mapStateToProps(state) {
  return {
    jobs: state.jobs
  };
}

function mapDispatchToProps(dispatch) {
  return {
    actions: {
      loadJobs: bindActionCreators(jobActions.loadJobs, dispatch),
      createJob: bindActionCreators(jobActions.createJob, dispatch)
    }
  };
}

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(JobsPage);
