#!/bin/bash
# set -eo pipefail
FUNCTION=$(aws --profile my-lambda cloudformation describe-stack-resource --stack-name danilo-blank-csharp --logical-resource-id function --query 'StackResourceDetail.PhysicalResourceId' --output text)

while true; do
  aws --profile my-lambda lambda invoke --function-name $FUNCTION --cli-binary-format raw-in-base64-out --payload file://event.json out.json
  cat out.json
  echo ""
  sleep 2
done
