#!/bin/bash
# set -eo pipefail
FUNCTION=$(aws cloudformation describe-stack-resource --stack-name danilo-blank-csharp --logical-resource-id function --query 'StackResourceDetail.PhysicalResourceId' --output text)

while true; do
  aws lambda invoke --function-name $FUNCTION
  cat out.json
  echo ""
  sleep 2
done
