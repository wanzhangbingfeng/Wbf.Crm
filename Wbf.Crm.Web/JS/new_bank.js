function onLoad() {
    return;
    var Sdk = {};
    Sdk.BankName = "建设银行";
    // NOTE: The getMetadata property should be attached to the function prototype instead of the
    // function object itself.
    Sdk.getMetadata = function () {
        return {
            boundParameter: null,
            parameterTypes: {
                "BankName": {
                    "typeName": "Edm.String",
                    "structuralProperty": 1 // Primitive Type
                }
            },
            operationType: 0, // This is an action. Use '1' for functions and '2' for CRUD
            operationName: "new_flowsubmit",
        };
    };

    // Use the request object to execute the function
    Xrm.WebApi.online.execute(Sdk)
        .then(function (response) {
        if (response.ok) {
            alert("执行成功");
        }
        }, function (error) {
            alert(error.message);
        });
}

function onSave() {
    var FieldRequest = {};
    FieldRequest.SolutionName = "test";
    FieldRequest.getMetadata = function () {
        return {
            boundParameter: null,
            parameterTypes: {
                "SolutionName": {
                    "typeName": "Edm.String",
                    "structuralProperty": 1 // Primitive Type
                }
            },
            operationType: 0, // This is an action. Use '1' for functions and '2' for CRUD
            operationName: "new_approve_field",
        };
    };

    // Use the request object to execute the function
    Xrm.WebApi.online.execute(FieldRequest)
        .then(function (response) {
            if (response.ok) {
                alert("执行成功");
            }
        }, function (error) {
            alert(error.message);
        });
}