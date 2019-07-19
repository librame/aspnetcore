import React, { Component } from 'react';
import GraphiQL from 'graphiql';
import fetch from 'isomorphic-fetch';
import 'graphiql/graphiql.css';
import './ApiExample.css';

export class ApiExample extends Component {
    static displayName = ApiExample.name;

    static graphQLFetcher(graphQLParams) {
        return fetch('api/graphql', {
            method: 'post',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(graphQLParams)
        }).then(response => response.json());
    }

    render() {
        return (
            <GraphiQL fetcher={ApiExample.graphQLFetcher} />
        );
    }
};