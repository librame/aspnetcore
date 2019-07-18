import React, { Component } from 'react';
import 'graphiql/graphiql.css';

export class GraphiQL extends Component {
    static displayName = GraphiQL.name;

    constructor(props) {
        super(props);
        this.state = { graphQLParams: [], loading: true };

        fetch('api/GraphQL')
            .then(response => response.json())
            .then(data => {
                this.setState({ graphQLParams: data, loading: false });
            });
    }

    static renderQuery(graphQLParams) {
        return (
            JSON.stringify(graphQLParams)
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : GraphiQL.renderQuery(this.state.graphQLParams);

        return (
            <div>
                <h1>Weather forecast</h1>
                <p>This component demonstrates graphql from the server.</p>
                {contents}
            </div>
        );
    }
};