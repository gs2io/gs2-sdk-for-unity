﻿﻿#!/bin/bash

rm *.tgz*

FILE_NAME=`npm pack`
curl -F package=@${FILE_NAME} https://${PUBLISH_KEY}@${HOST_NAME}/${PROJECT_NAME}/
