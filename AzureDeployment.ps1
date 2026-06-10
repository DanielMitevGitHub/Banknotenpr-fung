az group create -n MVC_Eurobanknoten --location germanywestcentral
az group list

az appservice plan create -n MVC_EurobanknotenPlan -g MVC_Eurobanknoten --location germanywestcentral --sku F1
az appservice plan list -o table

az webapp create -g MVC_Eurobanknoten -p MVC_EurobanknotenPlan -n MVC-Eurobanknoten-Server
az webapp list --query "[].{Name:name, State:state, Location:location, Url:defaultHostName}" -o table
