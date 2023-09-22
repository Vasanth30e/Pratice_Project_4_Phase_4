pipeline {
    agent any
    
    stages {
        stage('Checkout') {
            steps {
                checkout scm
            }
        }
        
        stage('Build') {
            steps {
                bat 'dotnet build'
            }
        }
        
        stage('Test') {
            steps {
                bat 'dotnet test'
            }
        }
        
        stage('Publish') {
            steps {
                bat 'dotnet publish -c Release -o ./publish'
            }
        }
    }
    
    post {
        failure {
            emailext (
                subject: "Pipeline Failed",
                body: "There was an error in the Jenkins pipeline. Please investigate.",
                to: "vasanth2219e@gmail.com"
            )
        }
    }
}
