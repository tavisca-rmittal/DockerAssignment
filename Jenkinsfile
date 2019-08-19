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
            defaultValue: "HiHellApi.dll",
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
                    dotnet restore %SOLUTION_PATH% --source https://api.nuget.org/v3/index.json
                    echo '=====================Restore Completed============'
                    echo '====================Build  Start ================'
                    dotnet build 
                    echo '=====================Build  Completed============'
                    echo '====================Test Start ================'
                    dotnet test %TEST_SOLUTION_PATH%
                    echo '=====================test Completed============'
                    echo '====================Publish Start at docker hub ================'
                    docker login -u %DOCKER_USER_NAME% -p %DOCKER_PASSWORD%
	            docker tag python:dockerimage %DOCKER_USER_NAME%/web_api_docker
	            docker push %DOCKER_USER_NAME%/web_api_docker:dockerimage
                    echo '=====================Publish Completed============'
                
                '''
            }
        }
             stage ('Deploy') {
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
