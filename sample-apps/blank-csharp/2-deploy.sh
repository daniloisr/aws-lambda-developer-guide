#!/bin/bash
set -eo pipefail
ARTIFACT_BUCKET=$(cat bucket-name.txt)
cd src/blank-csharp
dotnet lambda package
cd ../../
aws --profile my-lambda cloudformation package --template-file template.yml --s3-bucket $ARTIFACT_BUCKET --output-template-file out.yml
aws --profile my-lambda cloudformation deploy --template-file out.yml --stack-name danilo-blank-csharp --capabilities CAPABILITY_NAMED_IAM
