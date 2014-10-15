////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////
// 
// Module: ServiceFolder.cs
//
// 07-Oct-14: Version 1.0: Created
// 07-Oct-14: Version 1.0: Support for folders and their permissions
//
////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

namespace ev9 
{
   class ServiceFolder : ServiceItem
   {

      // Member Variables
      private string m_folder_path;

      private ServiceFile[] m_files;

      private Permission m_permission;

      // Constructors
      public ServiceFolder(string path, Permission permission)
      {
         Ctor(path);

         m_permission = permission;
      }

      public ServiceFolder(string path)
      {
         Ctor(path);
      }
         
      public string GetFolderPath()
      {
         return m_folder_path;
      }

      public Permission GetPermission()
      {
         return m_permission;
      }

      public void SetPermission(Permission permission)
      {
         m_permission = permission;
      }

      public override ServiceFile[] GetItems()
      {
         return m_files;
      }

      private void Ctor(string path)
      {
         m_folder_path = path;

         string[] filenames = Directory.GetFiles(m_folder_path, "*.*", SearchOption.AllDirectories);

         int count = 0;

         List<ServiceFile> files = new List<ServiceFile> ();

         foreach (string filename in filenames)
         {
            string file_path = Path.GetDirectoryName(filename);

            file_path = Path.GetFileName (file_path);

            string index_path = Path.GetFileName (filename);

            // ignore all hidden files
            if (index_path [0] == '.') 
            {
               continue;
            }

            index_path = "/" + file_path + "/" + index_path;

            files.Add(new ServiceFile(index_path, filename, m_permission));
         }

         m_files = new ServiceFile[files.Count];

         foreach (ServiceFile file in files) 
         {
            m_files [count++] = file;
         }

      }

   } // end of class(ServiceFolder)

} // end of namespace(ev9)

////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////
