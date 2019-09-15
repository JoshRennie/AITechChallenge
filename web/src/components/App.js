import React from "react";
import "../App.css";
import { BrowserRouter as Router, Route, Switch } from "react-router-dom";
import PageNotFound from "./PageNotFound";
import JobsPage from "./Jobs/JobsPage";

function App() {
  return (
    <Router>
      <Switch>
        <Route path="/jobs" component={JobsPage} />
        <Route path="/job/:id" />
        <Route component={PageNotFound} />
      </Switch>
    </Router>
  );
}

export default App;
