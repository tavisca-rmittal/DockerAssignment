pipeline{
    agent { label 'master' }
    parameters{
        string(
            name: "GIT_PATH",
            defaultValue: "https://github.com/tavisca-rmittal/WebApi.git",
            description: "GIT HTTPS PATH"
        )
        string(
            name: "SOLUTION_PATH",
            defaultValue: "HiHelloApi.sln",
            description: "SOLUTION_PATH"
        )
        
        string(
            name: "TEST_SOLUTION_PATH",
            defaultValue: "HiHelloApi.Tests/HiHelloApi.Tests.csproj",
            description: "TEST SOLUTION PATH"
        )
        
        string(                                             
            name: "PROJECT_PATH",
            defaultValue: "HiHelloApi.Tests/HiHelloApi.Tests.csproj",
            description: "TEST SOLUTION PATH"
        )
        
    
         string(
            name: "SOLUTION_DLL_FILE",
            defaultValue: "API.dll",
        )
        string(
            name: "DOCKER_USER_NAME",
            description: "Enter Docker hub Username"
        )
        string(
            name: "DOCKER_PASSWORD",
            description:  "Enter Docker hub Password"
        )
        choice(
            name: "RELEASE_ENVIRONMENT",
            choices: ["Build","Deploy"],
            description: "Tick what you want to do"
        )
    }
    stages{
        stage('Build'){
            when{
                expression{params.RELEASE_ENVIRONMENT == "Build" || params.RELEASE_ENVIRONMENT == "Deploy"}
            }
            steps{
                bat '''
                    echo '====================Restore Start ================'
                    dotnet restore ${SOLUTION_PATH} --source https://api.nuget.org/v3/index.json
                    echo '=====================Restore Completed============'
                    echo '====================Build DockerImage Start ================'
                    dotnet build --tag=dockerimage .
                    echo '=====================Build Project Completed============'
                    echo '====================Test Start ================'
                    dotnet test ${TEST_SOLUTION_PATH}
                    echo '=====================test Completed============'
                    echo '====================Publish Start at docker hub ================'
                    docker login -u ${DOCKER_USER_NAME} -p ${DOCKER_PASSWORD}
				    docker push tag dockerimage ridhima1998/web_api_docker
				    docker push ridhima1998/web_api_docker
                    echo '=====================Publish Completed============'
                
                '''
            }
        }
             stage ('Creating Docker Image') {
            when{
                expression{params.RELEASE_ENVIRONMENT == "Deploy"}
            }
            steps {
                bat '''
                echo '===============Deploying using Docker==========='
                docker run -p 8006:80 dockerimage
                '''
            }
        }
    }
    post{
        always{
            deleteDir()
       }
    }
}
