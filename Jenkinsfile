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
            name: "PROJECT_NAME", 
            defaultValue: "HiHelloApi"
            
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
        string(
            name: "SONAR_PROJECT_TOKEN",           
            description: "Enter Sonarqube token"
        )
         string(
            name: "MSBUILD_DLL_PATH",           
            defaultValue: "C:/Users/rmittal/Desktop/TAVISCA/sonarqube-7.9.1/sonar-scanner-msbuild-4.6.2.2108-netcoreapp2.0/SonarScanner.MSBuild.dll"
        )

        choice(
            name: "STAGE_FOR_EITHER_BUILD_OR_DEPLOY",
            choices: ["Build","Deploy"],
            description: "Tick what you want to do"
        )
    }
    stages{
        stage('Build'){
            when{
                expression{params.STAGE_FOR_EITHER_BUILD_OR_DEPLOY == "Build" || params.STAGE_FOR_EITHER_BUILD_OR_DEPLOY == "Deploy"}
            }
            steps{
                bat '''
                    echo '=========Sonarqube begin=============='
                    dotnet %MSBUILD_DLL_PATH% begin /k:"web_api_using_sonarqube" /d:sonar.host.url="http://localhost:9000" /d:sonar.login=%SONAR_PROJECT_TOKEN%
                    echo '====================Restore Start ================'
                    dotnet restore %SOLUTION_PATH% --source https://api.nuget.org/v3/index.json
                    echo '=====================Restore Completed============'
                    echo '====================Build  Start ================'
                    dotnet build 
                    echo '=====================Build  Completed============'
                    echo '====================Test Start ================'
                    dotnet test %TEST_SOLUTION_PATH%
                    echo '=====================test Completed============'
                    dotnet %MSBUILD_DLL_PATH% end /d:sonar.login=%SONAR_PROJECT_TOKEN%
                    echo '============Sonarqube end======================'
		    
                    echo '====================Publish Start at docker hub ================'
                    docker login -u %DOCKER_USER_NAME% -p %DOCKER_PASSWORD%  
                    docker build --tag=mydockerimage .
		            docker push %DOCKER_USER_NAME%/web_api_docker:mydockerimage		    
                    echo '=====================Publish Completed============'
                
                '''            

            }
        }
             stage ('Deploy') {
            when{
                expression{params.STAGE_FOR_EITHER_BUILD_OR_DEPLOY == "Deploy"}
            }
            steps {                     
                bat '''
                echo '===============Deploying using Docker==========='
                docker run -p 8006:80 %DOCKER_USER_NAME%/web_api_docker:mydockerimage
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
