job "microservices" {
  datacenters = ["*"]
  group "auth-service" {
    network {
      # nomad auto assigns a host ip port be mapped
      port "auth-service-port" { to = 8080 }
    }

    task "build" {
      driver = "raw_exec"

      lifecycle {
        hook    = "prestart"
        sidecar = false
      }

      artifact {
        source      = "git::https://github.com/PBSW/SynopsisBackend.git"
        destination = "local/repo"
      }

      config {
        args = ["-c", "cd local/repo/ && exec docker build --file Backend/Services/AuthService/API/Dockerfile --tag auth-service:local ."]
  			command = "/bin/sh"
      }
    }

    task "docker-run" {
      driver = "docker"
      config {
        image = "auth-service:local"
        ports = ["auth-service-port"]
      }
      resources {
        cpu    = 512
        memory = 2048
      }
    }
  }

  group "user-service" {
    network {
      # nomad auto assigns a host ip port be mapped
      port "user-service-port" { to = 8080 }
    }

    task "build" {
      driver = "raw_exec"

      lifecycle {
        hook    = "prestart"
        sidecar = false
      }

      artifact {
        source      = "git::https://github.com/PBSW/SynopsisBackend.git"
        destination = "local/repo"
      }
      
       config {
        args = ["-c", "cd local/repo/ && exec docker build --file Backend/Services/UserService/API/Dockerfile --tag user-service:local ."]
  			command = "/bin/sh"
      }
    }

    task "docker-run" {
      driver = "docker"
      config {
        image = "user-service:local"
        ports = ["user-service-port"]
      }
      resources {
        cpu    = 512
        memory = 2048
      }
    }
  }

  group "todo-service" {
    network {
      # nomad auto assigns a host ip port be mapped
      port "todo-service-port" { to = 8080 }
    }

    task "build" {
      driver = "raw_exec"

      lifecycle {
        hook    = "prestart"
        sidecar = false
      }

      artifact {
        source      = "git::https://github.com/PBSW/SynopsisBackend.git"
        destination = "local/repo"
      }
      
      config {
        args = ["-c", "cd local/repo/ && exec docker build --file Backend/Services/ToDoService/API/Dockerfile --tag todo-service:local ."]
  			command = "/bin/sh"
      }
    }

    task "docker-run" {
      driver = "docker"
      config {
        image = "todo-service:local"
        ports = ["todo-service-port"]
      }
      resources {
        cpu    = 512
        memory = 2048
      }
    }
  }

  group "item-service" {
    network {
      # nomad auto assigns a host ip port be mapped
      port "item-service-port" { to = 8080 }
    }

    task "build" {
      driver = "raw_exec"
      lifecycle {
        hook    = "prestart"
        sidecar = false
      }

      artifact {
        source      = "git::https://github.com/PBSW/SynopsisBackend.git"
        destination = "local/repo"
      }
      
      config {
        args = ["-c", "cd local/repo/ && exec docker build --file Backend/Services/ItemService/API/Dockerfile --tag item-service:local ."]
  			command = "/bin/sh"
      }
    }

    task "docker-run" {
      driver = "docker"
      config {
        image = "item-service:local"
        ports = ["item-service-port"]
      }
      resources {
        cpu    = 512
        memory = 2048
      }
    }
  }

  group "auth-db" {
    count = 1 
    service {
      name = "auth-db"
      port = "auth-db-port"
    }
    network {
      # nomad auto assigns a host ip port be mapped
      port "auth-db-port" { to = 3306 }
    }

    task "docker-run" {
      driver = "docker"
      config {
        image = "mysql:5.7"
        ports = ["auth-db-port"]
        volumes = ["lib/mysql:/var/lib/mysql"]
      }
      env {
        MYSQL_ROOT_PASSWORD = "root"
        MYSQL_DATABASE = "db"
        MYSQL_USER = "database_user"
        MYSQL_PASSWORD = "Password1!"
      }
      resources {
        cpu    = 512
        memory = 2048
      }
    }
  }
  group "user-db" {
    count = 1 
    service {
      name = "user-db"
      port = "user-db-port"
    }
    network {
      # nomad auto assigns a host ip port be mapped
      port "user-db-port" { to = 3306 }
    }

    task "docker-run" {
      driver = "docker"
      config {
        image = "mysql:5.7"
        ports = ["user-db-port"]
        volumes = ["lib/mysql:/var/lib/mysql"]
      }
      env {
        MYSQL_ROOT_PASSWORD = "root"
        MYSQL_DATABASE = "db"
        MYSQL_USER = "database_user"
        MYSQL_PASSWORD = "Password1!"
      }
      resources {
        cpu    = 512
        memory = 2048
      }
    }
  }
  group "todo-db" {
    count = 1
    service {
      name = "todo-db"
      port = "todo-db-port"
    }
    network {
      # nomad auto assigns a host port to be mapped
      port "todo-db-port" { to = 3306 }
    }
    task "docker-run" {
      driver = "docker"
      config {
        image = "mysql:5.7"
        ports = ["todo-db-port"]
        volumes = ["lib/mysql:/var/lib/mysql"]
      }
      env {
        MYSQL_ROOT_PASSWORD = "root"
        MYSQL_DATABASE = "db"
        MYSQL_USER = "database_user"
        MYSQL_PASSWORD = "Password1!"
      }
      resources {
        cpu    = 512
        memory = 2048
      }
    }
  }
}
