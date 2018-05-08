  using Microsoft.Extensions.Options;
  
  namespace <%= data.schema.appconfig.name %>.Infrastructure {
      public class PagingSettings{

      
           // Default pagination values
          private const int _DEFAULT_PAGESIZE = 5;
          private const int _DEFAULT_PAGE = 1;

          public int? pagesize { get; set; }
          public int? page { get; set; }

          public PagingSettings(int? pagesize, int? page){
              this.pagesize = pagesize;
              this.page = page;              
          }


          public PagingSettings(){
              this.pagesize = _DEFAULT_PAGESIZE;
              this.page = _DEFAULT_PAGE;
          }

      }
      public class QueryParameters {

          public QueryParameters (PagingSettings pagingSettings, string filter, string filterValues, string group, string order, string related, string fields) {
              
            
              this.pagingSettings = pagingSettings;
              this.fields = fields;
              this.filter = filter;
              this.filterValues = filterValues;
              this.order = order;
              this.group = group;
              this.related = related;
          }

          public QueryParameters () {

          }

          public PagingSettings pagingSettings;
          public string filter { get; set; }
          public string filterValues { get; set; }
          public string group { get; set; }
          public string order { get; set; }
          public string related { get; set; }
          public string fields { get; set; }

      }
  }