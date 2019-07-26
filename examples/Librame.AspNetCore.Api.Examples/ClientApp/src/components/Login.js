import React, { Component } from 'react';
import { Checkbox, TextField, Label, CompoundButton, PrimaryButton } from 'office-ui-fabric-react';
import 'bootstrap/dist/css/bootstrap.css';

export class Login extends Component {
    static displayName = Login.name;

    constructor(props) {
        super(props);
        this.state = {
            name: "",
            password: "",
            rememberMe: false,
            message: "Ready"
        };
    };

    submitForm = () => {
        const query = `
            mutation($model: LoginInput!) {
                login(user: $model) {
                    name
                    message
                    isError
                }
            }
        `;

        this.setState({
            message: "Loading..."
        });

        fetch('api/graphql', {
            method: 'post',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ query, variables: { model: this.state } })
        })
            .then(response => response.json())
            .then(data => {
                this.setState(data.data.login); // GraphiQL Response Schema
            });
    };

    inputChange = event => {
        var changed = (event.target.checked)
            ? event.target.checked
            : event.target.value;

        this.setState({
            [event.target.name]: changed
        });
    };

    render() {
        return (
            <div>
                <h1>GraphiQL Login</h1>

                <form className="form-horizontal">
                    <div>{this.state.message}</div>

                    <div className="form-group">
                        <Label>用户：</Label>
                        <TextField label="name" required
                            onChange={this.inputChange}
                        />
                    </div>

                    <div className="form-group">
                        <Label>密码：</Label>
                        <TextField label="password" required
                            onChange={this.inputChange}
                        />
                    </div>

                    <div className="form-group">
                        <Checkbox
                            label="rememberMe"
                            onChange={this.inputChange}
                        />
                    </div>

                    <div>
                        <PrimaryButton
                            data-automation-id="test"
                            text="Login"
                            onClick={this.submitForm}
                        />
                    </div>

                    <div>
                        <div>
                            <CompoundButton secondaryText="You can create a new account here.">
                                Register
                            </CompoundButton>
                        </div>
                        <div>
                            <CompoundButton secondaryText="You can reset a account here.">
                                ForgotPassword
                            </CompoundButton>
                        </div>
                    </div>
                </form>
            </div>
        );
    };
};