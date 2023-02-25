using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace AdoNetConectado
{
    public partial class frmTipoCliente : Form
    {
        string cadenaConexion = @"server=localhost\sqlexpress;DataBase=BancoBD; Integrated Security=true";
        public frmTipoCliente()
        {
            InitializeComponent();
        }

        private void cargarFormulario(object sender, EventArgs e)
        {
            cargarDatos();
        }


        private void cargarDatos()
        {
            // USING DELIMITA EL CICLO DE VIDA DE UNA VARIABLE
            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (var comando = new SqlCommand("SELECT * FROM TipoCliente", conexion))
                {
                    using (var lector = comando.ExecuteReader())
                    {
                        if (lector != null && lector.HasRows)
                        {
                            while (lector.Read())
                            {
                                dgvDatos.Rows.Add(lector[0], lector[1], lector[2], lector[3]);
                            }
                        }
                    }
                }
            }
        }

        private void nuevoRegistro(object sender, EventArgs e)
        {
            frmTipoClienteEdit frm = new frmTipoClienteEdit();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                string nombre = ((TextBox)frm.Controls["txtNombre"]).Text;
                string descripcion = ((TextBox)frm.Controls["txtDescripcion"]).Text;
                // OPERADOR TERNARIO
                var estado = ((CheckBox)frm.Controls["chkEstado"]).Checked == true ? 1 : 0;

                using (var conexion = new SqlConnection(cadenaConexion))
                {
                    conexion.Open();
                    using (var comando = new SqlCommand("INSERT INTO TipoCliente (Nombre, Descripcion, Estado) " +
                        "VALUES (@nombre, @descripcion, @estado)", conexion))
                    {
                        comando.Parameters.AddWithValue("@nombre", nombre);
                        comando.Parameters.AddWithValue("@descripcion", descripcion);
                        comando.Parameters.AddWithValue("@estado", estado);
                        int resultado = comando.ExecuteNonQuery();
                        if (resultado > 0)
                        {
                            
                          
                          
                            MessageBox.Show("Datos registrados.", "Sistemas",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("No se ha podido registrar los datos.", "Sistemas",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            dgvDatos.Rows.Clear();
            dgvDatos.Rows.AddRange();
            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (var comando = new SqlCommand("SELECT * FROM TipoCliente", conexion))
                {
                    using (var lector = comando.ExecuteReader())
                    {
                        if (lector != null && lector.HasRows)
                        {
                            while (lector.Read())
                            {
                                dgvDatos.Rows.Add(lector[0], lector[1], lector[2], lector[3]);
                            }
                        }
                    }
                }
            }
          
        }

        private void editarRegistro(object sender, EventArgs e)
        {
            // VALIDAMOS QUE EXISTAN FILAS PARA EDITAR
            if (dgvDatos.RowCount > 0 && dgvDatos.CurrentRow != null)
            {
                // TOMAMOS EL ID DE LA FILA SELECCIONADA
                int idTipo = int.Parse(dgvDatos.CurrentRow.Cells[0].Value.ToString());
                var frm = new frmTipoClienteEdit(idTipo);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    string nombre = ((TextBox)frm.Controls["txtNombre"]).Text;
                    string descripcion = ((TextBox)frm.Controls["txtDescripcion"]).Text;
                    // OPERADOR TERNARIO
                    var estado = ((CheckBox)frm.Controls["chkEstado"]).Checked == true ? 1 : 0;

                    using (var conexion = new SqlConnection(cadenaConexion))
                    {
                        conexion.Open();
                        using (var comando = new SqlCommand("UPDATE TipoCliente SET Nombre = @nombre, " +
                            "Descripcion = @descripcion, Estado = @estado WHERE ID = @id", conexion))
                        {
                            comando.Parameters.AddWithValue("@nombre", nombre);
                            comando.Parameters.AddWithValue("@descripcion", descripcion);
                            comando.Parameters.AddWithValue("@estado", estado);
                            comando.Parameters.AddWithValue("@id", idTipo);
                            int resultado = comando.ExecuteNonQuery();
                            if (resultado > 0)
                            {
                                MessageBox.Show("Datos actualizados.", "Sistemas",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("No se ha podido actualizar los datos.", "Sistemas",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            dgvDatos.Rows.Clear();
            dgvDatos.Rows.AddRange();
            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (var comando = new SqlCommand("SELECT * FROM TipoCliente", conexion))
                {
                    using (var lector = comando.ExecuteReader())
                    {
                        if (lector != null && lector.HasRows)
                        {
                            while (lector.Read())
                            {
                                dgvDatos.Rows.Add(lector[0], lector[1], lector[2], lector[3]);
                            }
                        }
                    }
                }
            }

        }

        private void EliminarRegistro(object sender, EventArgs e)
        {
           
           int idTipo= Convert.ToInt32(dgvDatos.SelectedRows[0].Cells[0].Value);
         //   String Nombre = (string)dgvDatos.SelectedRows[1].Cells[1].Value;
        //    String Descripcion = (string)dgvDatos.SelectedRows[2].Cells[2].Value;
            using (var conexion2 = new SqlConnection(cadenaConexion))
            {
                conexion2.Open();
                using (var comando2= new SqlCommand("DELETE FROM TipoCliente  WHERE ID = @id", conexion2))
                {
               //     comando2.Parameters.AddWithValue("@nombre", Nombre);
               //     comando2.Parameters.AddWithValue("@descripcion", Descripcion);        
                    comando2.Parameters.AddWithValue("@id", idTipo);
                    comando2.ExecuteNonQuery();
                }
            }
            dgvDatos.Rows.Clear();
            cargarDatos();











        }
    }
}
                   

            
      


                
                

     


                    
                
            
           

        
    
     
    

