const jwt = require('jsonwebtoken');

const SECRET_KEY = 'ThisIsStupidAndShouldntBeStoredHere';
const ISSUER = 'your_issuer';
const AUDIENCE = 'your_audience';

exports.handler = async (event) => {
    const token = event.authorizationToken;
    if (!token) {
        return generatePolicy('Deny', event.methodArn);
    }

    try {
        const decoded = jwt.verify(token, SECRET_KEY, {
            issuer: ISSUER,
            audience: AUDIENCE
        });

        const principalId = decoded.sub;
        return generatePolicy('Allow', event.methodArn, principalId);
    } catch (err) {
        console.error("Token verification error:", err);
        return generatePolicy('Deny', event.methodArn);
    }
};

function generatePolicy(effect, resource, principalId = 'user') {
    return {
        principalId,
        policyDocument: {
            Version: '2012-10-17',
            Statement: [
                {
                    Action: 'execute-api:Invoke',
                    Effect: effect,
                    Resource: resource
                }
            ]
        }
    };
}
