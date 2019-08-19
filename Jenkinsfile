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
                powershell '''
                    echo '====================Restore Start ================'
                    dotnet restore ${SOLUTION_PATH} --source https://api.nuget.org/v3/index.json
                    echo '=====================Restore Completed============'
                    echo '====================Build Project Start ================'
                    dotnet build ${PROJECT_PATH} 
                    echo '=====================Build Project Completed============'
                     echo '====================Test Start ================'
                    dotnet test ${TEST_SOLUTION_PATH}
                    echo '=====================test Completed============'
                     echo '====================Publish Start ================'
                    dotnet publish ${PROJECT_PATH}
                    echo '=====================Publish Completed============'
                
                '''
            }
        }
             stage ('Creating Docker Image') {
            when{
                expression{params.RELEASE_ENVIRONMENT == "Deploy"}
            }
            steps {
                powershell '''
                echo '===============Deploying using Docker==========='
                docker build -t docker_api_image .
                docker run -rm -p 8006:80 docker_api_image
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