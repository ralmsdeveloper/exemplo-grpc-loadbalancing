docker rmi app-microservice:latest
docker rmi app-frontend:latest

docker-compose -f .\docker\docker-compose.yml build app-frontend
docker-compose -f .\docker\docker-compose.yml build app-microservice

kubectl delete -f .\k8s\frontend.yml
choice /C YN /D Y /t 5
minikube image rm app-frontend:latest
kubectl apply -f .\k8s\frontend.yml

kubectl delete -f .\k8s\microservice.yml
choice /C YN /D Y /t 5
minikube image rm app-microservice:latest
kubectl apply -f .\k8s\microservice.yml
 
minikube image load app-frontend:latest
minikube image load app-microservice:latest