namespace <%= data.schema.appconfig.name %>.Infrastructure {
  public class AppConfig
  {
      
     public int pageSize {get; set; }
     public string queryStringDelimiter {get; set; }
     public string apiTitle {get; set; }
     public string apiVersion {get; set; }
     public string apiDocsPath  {get; set; }
     public string testAppPath {get; set; }

     public SecurityOptions securityOptions  {get; set; }

       public class SecurityOptions {
         public bool useSsl {get; set;}
         public int sslPort {get; set;}
         public string policyName {get; set;}
         public string sslCertificateFile {get; set;}
         public string sslPassword {get; set;}
      }

    
  }
}