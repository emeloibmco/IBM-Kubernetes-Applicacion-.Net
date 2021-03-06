# IBM Kubernetes-OpenShift Application .Net ☁📱

La presente guía está enfocada en el despliegue de una aplicación web ASP.NET Core MVC en Kubernetes y en OpenShift, junto con una base de datos SQL Server. 

## Índice  📰
1. [Pre-Requisitos](#Pre-Requisitos-pencil)
2. [Descripción y funcionamiento de la aplicación](#Descripción-y-funcionamiento-de-la-aplicación-mag_right)

### Sección 1 - Kubernetes
3. [Clonar Repositorio](#Clonar-Repositorio-pushpin-file_folder)
4. [Desplegar imagen de SQL Server en Kubernetes](#Desplegar-imagen-de-SQL-Server-en-Kubernetes-outbox_tray-cloud)
5. [Configurar cadena de conexión en aplicación](#Configurar-cadena-de-conexión-en-aplicación-hammer)
6. [Crear imagen de la aplicación](#Crear-imagen-de-la-aplicación-calling)
7. [Subir imagen de la aplicación a IBM Cloud Container Registry](#Subir-imagen-de-la-aplicación-a-IBM-Cloud-Container-Registry-cloud-books)
8. [Desplegar imagen de aplicación en Kubernetes](#Desplegar-imagen-de-aplicación-en-Kubernetes-cloud-rocket)
9. [Prueba de Funcionamiento en Kubernetes](#Prueba-de-Funcionamiento-en-Kubernetes-trophy)
10. [Visualizar tablas de base de datos en SSMS con Kubernetes](#Visualizar-tablas-de-base-de-datos-en-SSMS-con-Kubernetes-computer)

### Sección 2 - OpenShift
11. [Crear proyecto en OpenShift](#Crear-proyecto-en-OpenShift-notebook_with_decorative_cover-paperclip)
12. [Clonar Repositorio en IBM Cloud Shell](#Clonar-Repositorio-en-IBM-Cloud-Shell-pushpin-file_folder)
13. [Desplegar imagen de SQL Server en OpenShift](#Desplegar-imagen-de-SQL-Server-en-OpenShift-outbox_tray-cloud)
14. [Desplegar aplicación en OpenShift](#Desplegar-aplicación-en-OpenShift-cloud-rocket)
    * [Repositorio público de GitHub](#Repositorio-público-de-GitHub)
    * [Repositorio privado de Azure](#Repositorio-privado-de-Azure)
15. [Prueba de Funcionamiento en OpenShift](#Prueba-de-Funcionamiento-en-OpenShift-trophy)
16. [Visualizar tablas de base de datos en SSMS con OpenShift](#Visualizar-tablas-de-base-de-datos-en-SSMS-con-OpenShift-computer)
<br />

## Pre-requisitos :pencil:
* Contar con una cuenta en <a href="https://cloud.ibm.com/"> IBM Cloud </a>.
* Contar con un clúster en Kubernetes.
* Contar con un clúster en OpenShift de versión ```4.8``` para el despliegue de la aplicación con .NET 5.0. Si desea desplegar la aplicación en versión .NET Core 3.1 puede utilizar la  versión de OpenShift ```4.8 o una menor```.
* Tener instalado *Git* en su computador para clonar el respositorio.
* Tener instalada la CLI de *Docker*.
* Tener instalado *Docker Desktop* para verificar la creación de su imagen.
* Tener instalada la CLI de *IBM Cloud*.
* Tener instalada la CLI de <a href="https://cloud.ibm.com/docs/openshift?topic=openshift-openshift-cli#cli_oc"> OpenShift (oc)</a> en su computador para pruebas finales. Tenga en cuenta la versión de su clúster de OpenShift.
* Tener instalado <a href="https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver15"> SQL Server Management Studio </a> en su computador.
* Tener instalado Visual Studio 2019 en su computador.
<br />

## Descripción y funcionamiento de la aplicación :mag_right:
La demo que se presenta en este repositorio se desarrolló con ```.NET 5.0```. Se trata de una aplicación web ```ASP.NET Core MVC``` que integra los conceptos sobre modelos, vistas y controladores que permiten trabajar con aplicaciones dinámicas. Al ejecutar la aplicación puede observar que aparecen 5 pestañas: ```Inicio```, ```Transacciones```, ```Gastos```, ```Tipos de Gastos``` y ```Política de Privacidad```. A continuación, se presenta la explicación de cada una:
<br />

1. En la pestana ```Inicio``` aparece una ventana que da la bienvenida e indica que se trata de una aplicación desarrollada con ```ASP.NET Core```.
<p align="center"><img width="700" src="https://github.com/emeloibmco/IBM-Kubernetes-Applicacion-.Net/blob/main/Images/Inicio.PNG"></p>
<br />

2. En la pestaña ```Transacciones``` se habilita un botón que permite generar nuevas transacciones. 
<p align="center"><img width="700" src="https://github.com/emeloibmco/IBM-Kubernetes-Applicacion-.Net/blob/main/Images/Transacciones.PNG"></p>

Los campos que se pueden completar en la generación de una nueva transacción son:
* Nombre.
* Apellido.
* Ciudad.
* Dirección.
* Cédula.
* Fecha y Hora.
* Valor ($).
* Tipo de Transacción.

<br />
<p align="center"><img width="700" src="https://github.com/emeloibmco/IBM-Kubernetes-Applicacion-.Net/blob/main/Images/Nueva%20Transaccion.PNG"></p>
<br />

Una vez se ha generado una nueva transacción es posible editarla o eliminarla.
<p align="center"><img width="700" src="https://github.com/emeloibmco/IBM-Kubernetes-Applicacion-.Net/blob/main/Images/Transaccion1.PNG"></p>
<br />

3. En la pestaña ```Gastos``` se habilita un botón que permite generar nuevos gastos.
<p align="center"><img width="700" src="https://github.com/emeloibmco/IBM-Kubernetes-Applicacion-.Net/blob/main/Images/Gastos.PNG"></p>

Los campos que se pueden completar en la generación de un nuevo gasto son:
* Gasto.
* Valor ($).
* Tipo de Gasto.

<br />
<p align="center"><img width="700" src="https://github.com/emeloibmco/IBM-Kubernetes-Applicacion-.Net/blob/main/Images/Nuevo%20Gasto.PNG"></p>
<br />

Una vez se ha generado un nuevo gasto es posible editarlo o eliminarlo.
<p align="center"><img width="700" src="https://github.com/emeloibmco/IBM-Kubernetes-Applicacion-.Net/blob/main/Images/Gasto1.PNG"></p>
<br />

4. En la pestaña ```Tipos de Gastos``` se habilita un botón que permite generar nuevos tipos de gastos.
<p align="center"><img width="700" src="https://github.com/emeloibmco/IBM-Kubernetes-Applicacion-.Net/blob/main/Images/Tipos%20de%20Gastos.PNG"></p>

Los campos que se pueden completar en la generación de un nuevo tipo de gasto son:
* Tipo de Gasto.

<br />
<p align="center"><img width="700" src="https://github.com/emeloibmco/IBM-Kubernetes-Applicacion-.Net/blob/main/Images/Nuevo%20Tipo%20de%20Gasto.PNG"></p>
<br />

Una vez se ha generado un nuevo tipo gasto es posible editarlo o eliminarlo.
<p align="center"><img width="700" src="https://github.com/emeloibmco/IBM-Kubernetes-Applicacion-.Net/blob/main/Images/TipoGasto1.PNG"></p>


<br />

5. En la pestaña ```Política de Privacidad``` aparece una ventana que indica que la página web es una demo diseñada para pruebas.
<p align="center"><img width="700" src="https://github.com/emeloibmco/IBM-Kubernetes-Applicacion-.Net/blob/main/Images/Politicas.PNG"></p>

<br />

## Sección 1 - Kubernetes 💡
## Clonar Repositorio :pushpin: :file_folder:
La aplicación utilizada en esta guía la puede encontrar en este repositorio. Para clonar el repositorio en su computador, realice los siguientes pasos:

1. En su computador cree una carpeta a la que pueda acceder con facilidad y asígnele un nombre relacionado con la aplicación.
2. Abra una ventana de *Windows PowerShell* y vaya hasta la carpeta que creó en el ítem 1 con el comando ```cd```.
3. Una vez se encuentre dentro de la carpeta creada coloque el siguiente comando para clonar el repositorio:
```
git clone https://github.com/emeloibmco/IBM-Kubernetes-Applicacion-.Net.git
```
4. Acceda a la carpeta ```IBM-Kubernetes-Applicacion-.Net``` creada al clonar el repositorio y verifique que se encuentran descargados los archivos de la aplicación que se muestran en este repositorio.
<br />

## Desplegar imagen de SQL Server en Kubernetes :outbox_tray: :cloud:
Para realizar el despliegue de la imagen de SQL Server en Kubernetes, se utiliza *Persistent Volume Claims (PVC)*, que consiste en realizar una solicitud de almacenamiento a Kubernetes a un *Persistent Volume (PV)*. Este almacenamiento se puede solicitar en ```Mi (MB)``` o ```Gi (GB)```. 

Para este caso, se cuenta con 3 archivos de extensión ```.yaml```, que puede encontrar en la carpeta ```SQL Server - Despliegue en Kubernetes```. La explicación de cada archivo se presenta a continuación:

1. ```my-pvc.yaml```
```
apiVersion: v1
kind: PersistentVolumeClaim
metadata:
  name: mssql-pvc
spec:
  accessModes:
    - ReadWriteOnce
  resources:
    requests:
      storage: 1Mi
```
Este archivo es de tipo *PersistentVolumeClaim*. Allí se establece la respectiva configuración indicando: 
* Nombre: ```mssql-pvc```.
* Modo de acceso:  ```ReadWriteOnce``` para permitir que el *Persistent Volume* pueda ser leído y escrito por un solo nodo trabajador a la vez.
* Cantidad de almacenamiento, en este caso es de ```1 MB```.
<br />

2. ```sql-dep.yaml```
```
apiVersion: apps/v1
kind: Deployment
metadata:
  name: mssql-deployment
spec:
  replicas: 1
  selector:
     matchLabels:
       app: mssql
  template:
    metadata:
      labels:
        app: mssql
    spec:
      containers:
      - name: mssql
        image: mcr.microsoft.com/mssql/server:2019-latest
        ports:
        - containerPort: 1433
        env:
        - name: MSSQL_PID
          value: "Developer"
        - name: ACCEPT_EULA
          value: "Y"
        - name: SA_PASSWORD
          value: "<password>"
        volumeMounts:
        - name: mssqldb
          mountPath: ./data:/var/opt/mssql/data
      volumes:
      - name: mssqldb
        persistentVolumeClaim:
          claimName: mssql-pvc
```
Este archivo es de tipo *deployment*. Allí se establece la respectiva configuración indicando: 
* Nombre del despliegue: ```mssql-deployment```
* La imagen de SQL Server que se utilizará: ```mcr.microsoft.com/mssql/server:2019-latest```.
* El puerto de escucha TCP, por defecto es el ```1433```.
* Variables de entorno (*env*): estas variables deben coincidir con la cadena de conexión que se establece en la aplicación (ver [Configurar cadena de conexión en aplicación](#Configurar-cadena-de-conexión-en-aplicación-hammer)). Es importante reemplazar ```<password>``` con la contraseña establecida. En los archivos del repositorio se indicó un valor para la  contraseña, pero si desea puede modificarla.
* La ruta de montaje: se define la ruta dentro del contenedor donde se montará el *Persistent Volume*. Para este caso: ```./data:/var/opt/mssql/data```.
* El nombre del *Persistent Volume Claim* para realizar la solicitud: ```mssql-pvc```.
 <br />
  
3. ```sql-service.yaml``` 
```
apiVersion: v1
kind: Service
metadata:
  name: mssql-service
spec:
  selector:
    app: mssql
  type: NodePort  
  ports:
    - protocol: TCP
      port: 1433
      targetPort: 1433
```
Este archivo es de tipo *service*. Allí se establece la respectiva configuración indicando:
* Nombre del servicio: ```mssql-service```.
* Configuración de puertos: puerto ```1433``` que se abre en el servicio y puerto de destino ```1433``` para el contenedor del servidor SQL.
<br />

Una vez configurados y explicados los archivos necesarios, se deben emplear los siguientes comandos para realizar el despliegue de la imagen de SQL Server en el clúster de Kubernetes. Para ello, siga los pasos que se muestran a continuación:

1. En *Windows PowerShell* y con el comando ```cd``` vaya a los archivos de la carpeta ```SQL Server - Despliegue en Kubernetes``` (recuerde que está la encuentra luego de clonar el repositorio en su máquina local) y coloque:
```
ibmcloud login --sso
```
<br />

2. Seleccione la cuenta en donde se encuentra su clúster de Kubernetes.
<br />

3. Una vez ha iniciado sesión, configure el grupo de recursos y la región que está utilizando su clúster de Kubernetes. Para ello utilice el siguiente comando:
```
ibmcloud target -r <REGION> -g <GRUPO_RECURSOS>
```
>**Nota**: Reemplace \<REGION> y <GRUPO_RECURSOS> con su información.
<br />

4. Obtenga la lista de clústers de Kubernetes que hay en la cuenta establecida en el ítem 2:
```
ibmcloud cs clusters
```
<br />

5. Verifique el nombre del clúster en el que va a desplegar la imagen y habilite el comando ```kubectl``` de la siguiente manera:
```
ibmcloud ks cluster config --cluster <cluster_name>
```
<br />

6. Para crear un *Persistent Volume Claim (PVC)* utilice el comando:
```
kubectl apply -f my-pvc.yaml
```
<br />

Si desea observar el *Persistent Volume Claim (PVC)* que acaba de crear, utilice el comando:
```
kubectl get PersistentVolumeClaim
```
<br />

7. Posteriormente, se debe crear un despliegue de SQL Server en Kubernetes, que cuente con un *Persistent Volume (PV)* para almacenar los datos de la aplicación. Para ello coloque el comando:
```
kubectl apply -f sql-dep.yaml
```
<br />

8. Para finalizar, se debe crear un servicio para exponer el pod de SQL Server a otros pods en Kubernetes. Para ello coloque:
```
kubectl apply -f sql-service.yaml
```

Verifique en Kubernetes que aparezca:
* Persistent Volume Claims: ```mssql-pvc```
* Deployments: ```mssql-deployment```
* Services: ```mssql-service```
<br />

<p align="center"><img width="700" src="https://github.com/emeloibmco/IBM-Kubernetes-Applicacion-.Net/blob/main/Images/sql_Kubernetes.PNG"></p>


<br />

## Configurar cadena de conexión en aplicación :hammer:
Para realizar la respectiva conexión entre la aplicación y SQL Server en Kubernetes, se debe configurar la cadena de conexión teniendo en cuenta los parámetros establecidos al momento de desplegar la imagen de SQL Server. Para ello en el archivo ```appsettings.json``` que puede encontrar en ```IBM-Kubernetes-Applicacion-.Net/Application ASP.NET Core 5.0/InAndOut```, establezca los siguientes parámetros:

```
"ConnectionStrings": {
    "DefaultConnection": "Data Source=mssql-service,1433;Initial Catalog=MyNetDB;Persist Security Info=true;User ID=SA;Password=<password>; MultipleActiveResultSets=true"
  },
 ```
Considere lo siguiente:
 * Data Source = ```mssql-service,1433```, teniendo en cuenta el nombre del servicio expuesto para el Pod de SQL Server y el puerto de destino del contenedor.
 * Initial Catalog = ```MyNetDB```, corresponde al nombre de la base de datos en donde se va a almacenar la información. Si desea puede asignarle otro nombre.
 * Persist Security Info = ```true```
 * User ID = ```SA```, es el usuario. Por defecto deje este valor. 
 * Password = ```<password>```, en los archivos del repositorio se indicó un valor para la contraseña, pero si desea puede modificarla. Debe tener en cuenta que si modificó la contraseña en el item 2 del paso: [Desplegar imagen de SQL Server en Kubernetes](#Desplegar-imagen-de-SQL-Server-en-Kubernetes-outbox_tray-cloud), debe colocar en la cadena de conexión de la aplicación esa misma contraseña. 
 * MultipleActiveResultSets = ```true```

### NOTA: 
> ***SOLO*** en caso de realizar alguna modificación a la cadena de conexión presentada por defecto (por ejemplo: cambios en el nombre de la base de datos o la contraseña establecida en los archivos) debe realizar nuevamente las migraciones (son el proceso mediante el cual se mueven datos hacia o desde SQL Server). Para ello, realice lo siguiente:

* Elimine la carpeta ```Migrations``` que puede encontrar en ```IBM-Kubernetes-Applicacion-.Net/Application ASP.NET Core 5.0/InAndOut/```. 
* Abra el proyecto en Visual Studio 2019, de click en la ```Consola del Administrador de paquetes``` y coloque el siguiente comando:
```
add-migration <migration_name>
```
> Reemplace \<migration_name> con un nombre que le permita identificar la migración, por ejemplo: MigracionFinal.

<br />

<p align="center"><img width="700" src="https://github.com/emeloibmco/IBM-Kubernetes-Applicacion-.Net/blob/main/Images/ConsolaAdministradorPaquetes.PNG"></p>

* Teniendo en cuenta que realizó cambios en la aplicación, debe volver a publicarla. Para ello, elimine la carpeta ```Release``` que puede encontrar en ```IBM-Kubernetes-Applicacion-.Net/Application ASP.NET Core 5.0/InAndOut/bin/```. Posteriormente en *Windows PowerShell* y asegurandose de estar dentro de la carpeta ```InAndOut``` que puede encontrar en ```IBM-Kubernetes-Applicacion-.Net/Application ASP.NET Core 5.0/``` coloque el siguiente comando:
```
dotnet publish -c Release
``` 
Este comando le creará nuevamente la carpeta ```Release``` en ```IBM-Kubernetes-Applicacion-.Net/Application ASP.NET Core 5.0/InAndOut/bin/``` con las nuevas modificaciones realizadas a la aplicación.
 
<br />

## Crear imagen de la aplicación :calling:
Al clonar este repositorio puede encontrar dentro de los archivos el *Dockerfile* utilizado para crear la imagen de la aplicación. Realice los siguientes pasos:
1. En la ventaja de *Windows PowerShell* y asegurándose que se encuentra dentro de la carpeta que contiene los archivos de la aplicación (```InAndOut```) y el Dockerfile, coloque el siguiente comando para crear la imagen de su aplicación:
```
docker build -t <nombre_imagen:tag> .
```
> Nota: Reemplace <nombre_imagen:tag> con un nombre y una etiqueta que le permita identificar su imagen.
<br />

2. Una vez finalice el proceso, verifique en *Docker Desktop* que la imagen que acaba de crear aparece en la lista de imágenes.
<p align="center"><img width="700" src="https://github.com/emeloibmco/IBM-Kubernetes-Applicacion-.Net/blob/main/Images/DockerOk.PNG"></p>
<br />


## Subir imagen de la aplicación a IBM Cloud Container Registry :cloud: :books:
Para subir la imagen creada a *IBM Cloud Container Registry* realice lo siguiente:
1. En la ventana de *Windows PowerShell* inicie sesión en su cuenta de *IBM Cloud* con el siguiente comando:
```
ibmcloud login --sso
```
<br />

2. Seleccione la cuenta en donde se encuentra su clúster de Kubernetes.
<br />

3. Una vez ha iniciado sesión, configure el grupo de recursos y la región que está utilizando su clúster de Kubernetes. Para ello utilice el siguiente comando:
```
ibmcloud target -r <REGION> -g <GRUPO_RECURSOS>
```
>**Nota**: Reemplace \<REGION> y <GRUPO_RECURSOS> con su información.
<br />


4. Registre el daemon de Docker local en *IBM Cloud Container Registry* con el comando:
```
ibmcloud cr login
```
<br />

5. Cree un espacio de nombres (*namespace*) dentro de *IBM Cloud Container Registry* para su imagen. Para ello ejecute el siguiente comando:
```
ibmcloud cr namespace-add <namespace>
```
>**Nota**: Reemplace \<namespace> con un nombre fácil de recordar y que esté relacionado con la imagen de la aplicación. 
<br />

6. Elija un repositorio y una etiqueta con la que pueda identificar su imagen. En este caso, debe colocar la información de la imagen que creó en *Docker* y el espacio de nombres (*namespace*) creado en el ítem anterior. Coloque el siguiente comando:
```
docker tag <nombre_imagen:tag> us.icr.io/<namespace>/<nombre_imagen:tag>
```
>**Nota**: En el nombre de dominio **us.icr.io**, debe tener en cuenta colocar el dato correcto en base a la región en donde se encuentra su clúster y grupo de recursos. Para mayor información puede consultar <a href="https://cloud.ibm.com/docs/Registry?topic=Registry-registry_overview#registry_regions"> regiones </a>.
<br />

7. Envíe la imagen a *IBM Cloud Container Registry* mediante el comando:
```
docker push us.icr.io/<namespace>/<nombre_imagen:tag>
```
<br />

8. Verifique en *IBM Cloud Container Registry* que aparece el espacio de nombres (namespace), el repositorio y la imagen. Tenga en cuenta los nombres que asignó en cada paso.
<p align="center"><img width="700" src="https://github.com/emeloibmco/IBM-Kubernetes-Applicacion-.Net/blob/main/Images/CrOk.PNG"></p>

<br />



## Desplegar imagen de aplicación en Kubernetes :cloud: :rocket:
Para desplegar la imagen de la aplicación en Kubernetes, realice lo siguiente:
1. En la ventana de *Windows PowerShell* en la que ha trabajado, coloque el siguiente comando para ver la lista de clústers de Kubernetes que hay en su cuenta:
```
ibmcloud cs clusters
```
<br />

2. Verifique el nombre de clúster en el que va a desplegar la imagen y habilite el comando kubectl de la siguiente manera:
```
ibmcloud ks cluster config --cluster <cluster_name>
```
<br />

3. Cree el servicio de despliegue en Kubernetes, para esto, ejecute los comandos que se muestran a continuación (recuerde cambiar \<deployment> con un nombre para su servicio de despliegue):  
```
kubectl create deployment <deployment> --image=us.icr.io/<namespace>/<nombre_imagen:tag>
```
<br />
  
4. A continuación, debe exponer su servicio en Kubernetes, para ello realice lo siguiente.
>**NOTA 1**: Si esta trabajando con infraestructura clásica ejecute el siguiente comando:

```
kubectl expose deployment/<deployment> --type=NodePort --port=8080
```

>**NOTA 2**: Si esta trabajando con VPC (Load Balancer) ejecute el siguiente comando:
```
kubectl expose deployment/<deployment> --type=LoadBalancer --name=<service>  --port=8080 --target-port=8080
```
En la etiqueta **\<service>** indique un nombre para su servicio. Recuerde colocar el valor del puerto en base a lo establecido en el Dockerfile de la aplicación.

<br />


5. Por último verifique que el deployment y el service creados aparecen de forma exitosa en el panel de control de su clúster.
<p align="center"><img width="700" src="https://github.com/emeloibmco/IBM-Kubernetes-Applicacion-.Net/blob/main/Images/app_kubernetes.PNG"></p>
<br />



## Prueba de Funcionamiento en Kubernetes :trophy:
Para verificar el correcto funcionamiento de su aplicación en Kubernetes realice lo siguiente:

1. Si trabaja con infraestructura clásica su aplicación funcionará si coloca en el navegador ```IP_Publica:puerto```. Para obtener la IP Pública coloque el comando:
```
ibmcloud ks workers --cluster <ID_Cluster>
```

Para obtener el puerto use el comando:
```
kubectl get service <deployment>
```
<br />

2. Si trabaja con VPC (Load Balancer), diríjase a la pestaña Service/Services dentro del panel de control de Kubernetes, visualice el servicio creado y de click en el external endpoint.  
<br />

3. Visualice las diferentes ventanas de la aplicación y realice pruebas con datos en las secciones de ```Transferencias```, ```Gastos``` y ```Tipos de Gastos```.
<p align="center"><img width="700" src="https://github.com/emeloibmco/IBM-Kubernetes-Applicacion-.Net/blob/main/Images/App-Kubernetes.gif"></p>

<br />


## Visualizar tablas de base de datos en SSMS con Kubernetes :computer:
Para visualizar las tablas de la base de datos de forma local en *SQL Server Management Studio (SSMS)* realice lo siguiente:
1. En *Windows PowerShell* visualice el Pod de SQL Server mediante el comando:
```
kubectl get pods
```
Allí, observará la lista de Pods que se ejecutan en su clúster de Kubernetes. Visualice el Pod de SQL Server y copie el nombre para usarlo más adelante.
<br />

2. Reenvíe la conexión del puerto del Pod a un puerto local que no esté usando en su máquina, para ello utilice el comando:
```
kubectl port-forward pod/<pod_name> <puerto_local>:1433 
```
> **NOTA**: Reemplace <pod_name> con el nombre del Pod de SQL Server en Kuberntes y <puerto_local> con un puerto que no esté usando en su máquina local, por ejemplo: 15789. Como resultado, una vez ejecute el comando anterior obtendrá: *127.0.0.1:<puerto_local>*
<br />

3. Para visualizar las tablas de datos y la información registrada en las pruebas de funcionamiento, abra *SQL Server Management Studio* y complete los campos con la siguiente información:
* Service Type: ```Database Engine```.
* Server name: ```127.0.0.1,<puerto_local>```.
* Authentication: ```SQL Server Authentication```.
* Login:```SA```.
* Password: ```<password>```.

> **NOTA**: Recuerde reemplazar \<password> con la contraseña. Si dejó la que se estableció en los archivos de la aplicación, coloque la misma aquí, de lo contrario coloque la contraseña que asignó en los pasos [Desplegar imagen de SQL Server en Kubernetes](#Desplegar-imagen-de-SQL-Server-en-Kubernetes-outbox_tray-cloud)
y [Configurar cadena de conexión en aplicación](#Configurar-cadena-de-conexión-en-aplicación-hammer)

<p align="center"><img width="700" src="https://github.com/emeloibmco/IBM-Kubernetes-Applicacion-.Net/blob/main/Images/SSMS-Kuberntes.gif"></p>

<br />


## Sección 2 - OpenShift 💡
## Crear proyecto en OpenShift :notebook_with_decorative_cover: :paperclip:
Para empezar la creación de su proyecto, acceda a la consola web de OpenShift (en el clúster que va a trabajar) y asegurándose de estar en el rol de ```Developer```, de click en la pestaña ```Project```y luego ```Create Project```. Allí, asígne un nombre y de click en el botón ```Create```, como se observa en la imagen.
<br />
<p align="center"><img width="700" src="https://github.com/emeloibmco/IBM-Kubernetes-Applicacion-.Net/blob/main/Images/NewProject_OpenShift.gif"></p>
<br />

## Clonar Repositorio en IBM Cloud Shell :pushpin: :file_folder:
Una vez ha creado su proyecto, debe clonar el repositorio en IBM Cloud Shell. Para ello realice lo siguiente:
<br />
1. Acceda al IBM Cloud Shell (lo puede encontrar en el ícono que se muestra en la imagen).
<br />
<p align="center"><img width="900" src="https://github.com/emeloibmco/IBM-Kubernetes-Applicacion-.Net/blob/main/Images/IBMCloudShell.PNG"></p>
<br />

2. Dentro de la consola web de OpenShift, de click sobre su correo (parte superior derecha) y posteriormente en la opción ```Copy Login Command```. Una vez salga la nueva ventana, de click en la opción ```Display Token```y posteriormente copie el comando que sale en la opción ```Log in with this token``` y colóquelo en el IBM Cloud Shell para iniciar sesión.
<br />
<p align="center"><img width="700" src="https://github.com/emeloibmco/IBM-Kubernetes-Applicacion-.Net/blob/main/Images/AccesoIBMCloudShell.gif"></p>
<br />
<p align="center"><img width="700" src="https://github.com/emeloibmco/IBM-Kubernetes-Applicacion-.Net/blob/main/Images/AccesoIBMCloudShell2.PNG"></p>

<br />

3. Acceda al proyecto que creó anteriormente con el comando:
```
oc project <project_name>
```
<br />

4. Luego, debe clonar el repositorio actual con el comando:
```
git clone https://github.com/emeloibmco/IBM-Kubernetes-Applicacion-.Net.git
```
<br />
<p align="center"><img width="700" src="https://github.com/emeloibmco/IBM-Kubernetes-Applicacion-.Net/blob/main/Images/ClonarRepo.PNG"></p>

<br />


## Desplegar imagen de SQL Server en OpenShift :outbox_tray: :cloud:
Los archivos son los mismos utilizados en el paso [Desplegar imagen de SQL Server en Kubernetes](#Desplegar-imagen-de-SQL-Server-en-Kubernetes-outbox_tray-cloud). 
Para desplegar la imagen de SQL Server en OpenShift, utilice los siguientes comandos:
<br />

1. Dentro de IBM Cloud Shell, muévase con el comando ```cd``` hasta la carpeta que contiene los archivos ```.yaml``` de SQL Server. Estos archivos los encuentra en la carpeta: ```IBM-Kubernetes-Applicacion-.Net/SQL Server - Despliegue en Kubernetes/```
<br />

2. Debe crear un *Persistent Volume Claim (PVC)*, utilice el comando:
```
oc apply -f my-pvc.yaml
```
<br />

3. Posteriormente, debe crear un despliegue de SQL Server en OpenShift, que cuente con un *Persistent Volume (PV)* para almacenar los datos de la aplicación. Para ello coloque el comando:
```
oc apply -f sql-dep.yaml
```
<br />

4. Para finalizar, se debe crear un servicio para exponer el pod de SQL Server en OpenShift. Para ello coloque:
```
oc apply -f sql-service.yaml
```
<br />
<p align="center"><img width="700" src="https://github.com/emeloibmco/IBM-Kubernetes-Applicacion-.Net/blob/main/Images/SQLServer-OpenShift.PNG"></p>

Verifique que en su proyecto de OpenShift aparezca:
* Deployments: ```mssql-deployment```
* Services: ```mssql-service```

<br />
<p align="center"><img width="700" src="https://github.com/emeloibmco/IBM-Kubernetes-Applicacion-.Net/blob/main/Images/SQLServer_OK_OpenShift.PNG"></p>

<br />

## Desplegar aplicación en OpenShift :cloud: :rocket:
Para desplegar la imagen de la aplicación en OpenShift, utilice el mismo proyecto en el que desplegó la imagen de SQL Server, teniendo en cuenta que la aplicación se debe comunicar con el servicio ```mssql-service``` para almacenar los datos.
<br />

En el presente repositorio puede encontrar dos carpetas con la versión .NET 5.0 (```Application ASP.NET Core 5.0 - OpenShift```) y .NET Core 3.1 (```Application ASP.NET Core 3.1 - OpenShift```) de la aplicación que están listas para desplegarse en OpenShift. La diferencia con la carpeta que contiene el código usado para el despliegue de Kubernetes (```Application ASP.NET Core 5.0```), es la carpeta ```.s2i``` en la cual se encuentra el archivo ```environment```, que convierte el código fuente en una imagen de la aplicación directamente en OpenShift.

<br />

Recuerde que dependiendo de la versión de su clúster de OpenShift va a poder desplegar la aplicación en versión .NET 5.0 (```versión de OpenShift 4.8```) o .NET Core 3.1 (```versión de OpenShift 4.8 o menor```). Por otro lado, teniendo en cuenta el tipo de repositorio en el que tiene su aplicación cuenta con las siguientes opciones:

* [Repositorio público de GitHub](#Repositorio-público-de-GitHub)
* [Repositorio privado de Azure](#Repositorio-privado-de-Azure)

<br />

## Repositorio público de GitHub

* [Desplegar aplicación en versión 3 1](#Desplegar-aplicación-en-versión-3-1)
* [Desplegar aplicación en versión 5 0](#Desplegar-aplicación-en-versión-5-0)
<br />

### .NET Core 3.1
### Desplegar aplicación en versión 3 1
Dentro de su proyecto en la consola web de OpenShift, de click en la pestaña > ```+Add```  y seleccione la opción ```From Git```. En la URL del repositorio utilice <a href="https://github.com/DianaEspitia/Application-ASP.NET-3.1"> https://github.com/DianaEspitia/Application-ASP.NET-3.1</a>. (En caso de presentar fallas con el repositorio indicado, puede encontrar en la carpeta ```IBM-Kubernetes-Applicacion-.Net/Application ASP.NET Core 3.1 - OpenShift/``` de este repositorio la aplicación en versión ```3.1```, junto con la carpeta ```.s2i``` indicada. En ese caso, clone este repositorio en su máquina local y suba únicamente la aplicación ```.NET Core 3.1``` en un nuevo repositorio. Use la URL de su nuevo repositorio).

Luego, en la opción ```Builder Image``` seleccione ```.NET Core``` y en el ```Builder Image Version``` elija la opción ```latest``` o ```3.1```. Asigne un nombre para su aplicación y de click en el botón ```Create```. 

<br />
<p align="center"><img width="700" src="https://github.com/emeloibmco/IBM-Kubernetes-Applicacion-.Net/blob/main/Images/App-OpenShift.gif"></p>

<br />

### .NET 5.0
### Desplegar aplicación en versión 5 0
Dentro de su proyecto en la consola web de OpenShift, de click en la pestaña > ```+Add```  y seleccione la opción ```From Git```. En la URL del repositorio utilice <a href="https://github.com/DianaEspitia/Application-ASP.NET-5.0"> https://github.com/DianaEspitia/Application-ASP.NET-5.0</a>. (En caso de presentar fallas con el repositorio indicado, puede encontrar en la carpeta ```IBM-Kubernetes-Applicacion-.Net/Application ASP.NET Core 5.0 - OpenShift/``` de este repositorio la aplicación en versión ```5.0```, junto con la carpeta ```.s2i``` indicada. En ese caso, clone este repositorio en su máquina local y suba únicamente la aplicación ```.NET 5.0``` en un nuevo repositorio. Use la URL de su nuevo repositorio).

Luego, en la opción ```Builder Image``` seleccione ```.NET Core``` y en el ```Builder Image Version``` elija la opción ```5.0```. Asigne un nombre para su aplicación y de click en el botón ```Create```. 

<br />
<p align="center"><img width="700" src="https://github.com/emeloibmco/IBM-Kubernetes-Applicacion-.Net/blob/main/Images/DesplegarApp50.gif"></p>

<br />


Luego de completar los pasos con la versión seleccionada de la aplicación, espere unos minutos mientras se completa el despliegue. Cuando todo este listo, debe observar en su proyecto la aplicación .NET Core.

<br />
<p align="center"><img width="700" src="https://github.com/emeloibmco/IBM-Kubernetes-Applicacion-.Net/blob/main/Images/App-OK-OpenShift.PNG"></p>

<br />

## Repositorio privado de Azure
Si desea desplegar una aplicación cuyo código y archivos se encuentran en un repositorio privado de Azure, cuenta con 2 opciones:

* [SSH](#SSH)
* [HTTPS](#HTTPS)
<br />

### SSH
1. Cree una llave SSH y asociela a su repositorio de Azure. Para ello complete los siguientes pasos:

   * Abra una ventana en *IBM Cloud Shell* y coloque el comando:
     ```
     ssh-keygen -t rsa -C "user_id"
     ```
     <br />     
         
   * Al ejecutar el comando anterior, en la consola se pide que especifique la ubicación, en este caso oprima la tecla ```Enter``` para que se guarde en la ubicación sugerida. Posteriormente, cuando se pida la ```Passphrase``` oprima la tecla ```Enter``` para dejarlo vacio.
     <br />
     
     <p align="center"><img src="https://github.com/emeloibmco/IBM-Kubernetes-Applicacion-.Net/blob/main/Images/1_ssh_ibm.PNG"></p>

     <br />
      
   * Muévase a la carpeta ```ssh``` con el comando ```cd .ssh``` y visualice los archivos ```id_rsa.pub``` y ```id_rsa``` con el comando ```dir```. Estos archivos contienen las claves públicas y privadas respectivamente.
     <br />
     
     <p align="center"><img src="https://github.com/emeloibmco/IBM-Kubernetes-Applicacion-.Net/blob/main/Images/2_ssh_ibm.PNG"></p>

     <br />
      
   * Visualice la clave pública. Asegúrese de guardarla, ya que la necesitará más adelante. Utilice el comando:
      ```
     cat id_rsa.pub
     ```
     <br />
     
     <p align="center"><img src="https://github.com/emeloibmco/IBM-Kubernetes-Applicacion-.Net/blob/main/Images/3_ssh_ibm.PNG"></p>

     <br />
     
   * Visualice la clave privada. Asegúrese de guardarla, ya que la necesitará más adelante. Utilice el comando:
      ```
     cat id_rsa
     ```
     <br />
     
     <p align="center"><img src="https://github.com/emeloibmco/IBM-Kubernetes-Applicacion-.Net/blob/main/Images/4_ssh_ibm.PNG"></p>

     <br />

2. Asocie la clave SSH pública creada anteriormente con el respositorio de Azure. Para ello ingrese a ```User settings``` ➡ ```SSH public keys```, de click en ```+ New key``` e ingrese lo siguiente en los campos específicos:

   * ```Name```: Especifíque un nombre exclusivo para la clave SSH.
   * ```Public Key Data```: Ingrese la clave SSH pública generada en *IBM Cloud Shell*.
   
   <br />
     
   <p align="center"><img src="https://github.com/emeloibmco/IBM-Kubernetes-Applicacion-.Net/blob/main/Images/5_ssh-azure.gif"></p>

   <br />
   
3. En el repositorio de Azure de click en el botón ```Clone```. Luego en la sección ```SSH``` visualice el enlace del repositorio y guárdelo para usarlo en OpenShift.
   <br />
     
   <p align="center"><img src="https://github.com/emeloibmco/IBM-Kubernetes-Applicacion-.Net/blob/main/Images/7_ssh.gif"></p>

   <br />

4. Para desplegar su aplicación de click en el botón ```+Add``` ➡ ```Git Repository``` ➡ ```From Git```. 
<br />

5. Coloque el enlace del repositorio de Azure que aparece en la sección de SSH. 
<br />

6. De click en ```Show advanced Git options``` y genere un secreto. Para ello, en la sección ```Source Secret``` despliegue el ```Select Secret name``` y presione ```Create new Secret```. Complete los campos de la siguiente manera:

   * ```Secret name```: indique un nombre exclusivo para el secreto.
   * ```Authentication type```: seleccione como método de autenticación ***SSH Key***.
   * ```Drag and drop file with your private SSH key here or browse to upload it```: coloque la clave SSH privada que generó en *IBM Cloud Shell*.
   
   De click en el botón ```Create```.
         
   > NOTA: en caso de haber generado de forma previa un secreto con la clave SSH privada puede seleccionarlo. De lo contrario siga los pasos y cree uno.
   <br />
   
7. Seleccion en ```Builder Image``` la opción ```.NET```.
<br />

8. Seleccione la versión de .NET que utiliza su aplicación. En este caso ```5.0-ubi8```.
<br />

9. Indique un nombre para su aplicación y grupo de aplicaciones.
<br />

10. Deje los demás campos con los valores que aparecen por defecto y presione el botón ```Create```.
   
   <br />
     
   <p align="center"><img src="https://github.com/emeloibmco/IBM-Kubernetes-Applicacion-.Net/blob/main/Images/9_app_azure_ssh.gif"></p>

   <br />


### HTTPS
1. En el repositorio de Azure de click en el botón ```Clone```. Luego en la sección ```HTTPS``` visualice el enlace del repositorio y guárdelo para usarlo en OpenShift. Adicionalmente, de click en el botón ```Generate Git Credentials``` y guarde el usuario y contraseña que se muestran para acceder al repositorio.
   <br />
     
   <p align="center"><img src="https://github.com/emeloibmco/IBM-Kubernetes-Applicacion-.Net/blob/main/Images/6_https.gif"></p>

   <br />

2. Para desplegar su aplicación de click en el botón ```+Add``` ➡ ```Git Repository``` ➡ ```From Git```. 
<br />

3. Coloque el enlace del repositorio de Azure que aparece en la sección de HTTPS. 
<br />

4. De click en ```Show advanced Git options``` y genere un secreto. Para ello, en la sección ```Source Secret``` despliegue el ```Select Secret name``` y presione ```Create new Secret```. Complete los campos de la siguiente manera:

   * ```Secret name```: indique un nombre exclusivo para el secreto.
   * ```Authentication type```: seleccione como método de autenticación ***Basic Authentication***.
   * ```Username```: coloque el nombre de usuario que aparece en la credenciales de Azure.
   * ```Password or token```: coloque la contraseña que aparece en la credenciales de Azure.
   
   De click en el botón ```Create```.
         
   > NOTA: en caso de haber generado de forma previa un secreto con las credenciales de acceso al repositorio privado, puede seleccionarlo. De lo contrario siga los pasos y cree uno.
   <br />
   
5. Seleccion en ```Builder Image``` la opción ```.NET```.
<br />

6. Seleccione la versión de .NET que utiliza su aplicación. En este caso ```5.0-ubi8```.
<br />

7. Indique un nombre para su aplicación y grupo de aplicaciones.
<br />

8. Deje los demás campos con los valores que aparecen por defecto y presione el botón ```Create```.
   
   <br />
     
   <p align="center"><img src="https://github.com/emeloibmco/IBM-Kubernetes-Applicacion-.Net/blob/main/Images/8_app_azure_up.gif"></p>

   <br />



<br />


## Prueba de Funcionamiento en OpenShift :trophy:
Para verificar el correcto funcionamiento de su aplicación en OpenShift, de click en la opción ```Open URL``` y verifique que aparecen las diferentes ventanas de la aplicación. Luego realice pruebas con datos en las secciones de ```Transferencias```, ```Gastos``` y ```Tipos de Gastos```.

<br />
<p align="center"><img width="700" src="https://github.com/emeloibmco/IBM-Kubernetes-Applicacion-.Net/blob/main/Images/FuncionamientoApp-OpenShift.gif"></p>

<br />

## Visualizar tablas de base de datos en SSMS con OpenShift :computer:

Para visualizar las tablas de la base de datos de forma local en *SQL Server Management Studio (SSMS)* realice lo siguiente:
1. En el símbolo de sistema (*cmd*) de su computador inicie sesión en *IBM Cloud* con:
```
ibmcloud login --sso
```
<br />

2. Seleccione la cuenta en donde se encuentra su clúster de OpenShift.
<br />

3. Una vez ha iniciado sesión, configure el grupo de recursos y la región que está utilizando su clúster de OpenShift. Para ello utilice el siguiente comando:
```
ibmcloud target -r <REGION> -g <GRUPO_RECURSOS>
```
>**Nota**: Reemplace \<REGION> y <GRUPO_RECURSOS> con su información.
<br />

4. Posteriormente, inicie sesion en OpenShift. Para ello, dentro de la consola de OpenShift de click sobre su correo (parte superior derecha) y posteriormente en la opción ```Copy Login Command```. Una vez salga la nueva ventana, de click en la opción ```Display Token```y posteriormente copie el comando que sale en la opción ```Log in with this token``` y colóquelo en la consola *cmd*.
<br />

5. Si no se encuentra dentro del proyecto que está trabajando, acceda al mismo mediante el comando:
```
oc project <project_name>
```
<br />

6. Visualice el Pod de SQL Server mediante el comando:
```
oc get pods
```
Allí, observará la lista de Pods que se ejecutan en el proyecto establecido dentro del clúster de OpenShift. Visualice el Pod del SQL Server y copie el nombre para usarlo más adelante.
<br />

7. Reenvíe la conexión del puerto del Pod a un puerto local que no esté usando en su máquina, para ello utilice el comando:
```
oc port-forward pod/<pod_name> <puerto_local>:1433 
```
> **NOTA**: Reemplace <pod_name> con el nombre del Pod de SQL Server en OpenShift y <puerto_local> con un puerto que no esté usando en su máquina local, por ejemplo: 8888. Como resultado, una vez ejecute el comando anterior obtendrá: *127.0.0.1:<puerto_local>*
<br />

8. Para visualizar las tablas de datos y la información registrada en las pruebas de funcionamiento, abra *SQL Server Management Studio* y complete los campos con la siguiente información:
* Service Type: ```Database Engine```.
* Server name: ```127.0.0.1,<puerto_local>```.
* Authentication: ```SQL Server Authentication```.
* Login:```SA```.
* Password: ```<password>```.

> **NOTA**: Recuerde reemplazar \<password> con la contraseña establecida en la configuración de los archivos de SQL Server. Si desea deje los valores por defecto que se indicaron en los archivos de este reporitorio.

<p align="center"><img width="700" src="https://github.com/emeloibmco/IBM-Kubernetes-Applicacion-.Net/blob/main/Images/SSMS-OpenShift.gif"></p>

<br />

## Autores ✒
Equipo IBM Cloud Tech Sales Colombia.
