﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;//Archivos

namespace examenParcial1
{
    public partial class Form1 : Form
    {
        int n = 0;
        List<Departamentos> departamentos = new List<Departamentos>();
        List<Registros> registros = new List<Registros>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Cargar Archivo Departamentos
            FileStream stream = new FileStream("Departamentos.txt", FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);

            while (reader.Peek() > -1)
            {
                Departamentos departamentosTemp = new Departamentos();
                departamentosTemp.Codigo = reader.ReadLine();
                departamentosTemp.Departamento = reader.ReadLine();
                departamentos.Add(departamentosTemp);
            }
            reader.Close();

            //Carga el Archivo Registro
            stream = new FileStream("Registros.txt", FileMode.Open, FileAccess.Read);
            reader = new StreamReader(stream);

            while (reader.Peek() > -1)
            {
                Registros registrosTemp = new Registros();
                registrosTemp.Codigo = reader.ReadLine();
                for(int i=0; i < 5; i++)
                {
                    registrosTemp.Medicion.Add(reader.ReadLine());
                    registrosTemp.FechaMedicion.Add(reader.ReadLine());
                }
                registros.Add(registrosTemp);
            }
            reader.Close();

            //Mostrar Departamentos en ComboBox
            cmbDepartamentos.DisplayMember = "Departamento";
            cmbDepartamentos.ValueMember = "Codigo";
            cmbDepartamentos.DataSource = departamentos;
            cmbDepartamentos.Refresh();

            //Mostrar Lista departamentos
            dgvProm.DataSource = null;
            dgvProm.DataSource = departamentos;
            dgvProm.Refresh();

        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            if (registros.Count == 0)
            {
                //Crea el primer objeto de la lista
                Registros registrosTemp = new Registros();
                registrosTemp.Codigo = cmbDepartamentos.SelectedValue.ToString();
                registrosTemp.Medicion.Add(txtLluvia.Text);
                registrosTemp.FechaMedicion.Add(Convert.ToString(DateTime.Now));
                registros.Add(registrosTemp);
                Guardar();
            }
            else
            {
                //Bandera
                char bandera = 'F';

                for (int i = 0; i < registros.Count; i++)
                {
                    //Entra en si se repite el departamento
                    if (cmbDepartamentos.SelectedValue.ToString() == registros[i].Codigo)
                    {
                        registros[i].Medicion.Add(txtLluvia.Text);
                        registros[i].FechaMedicion.Add(Convert.ToString(DateTime.Now));
                        bandera = 'v';
                        Guardar();
                    }
                }

                //Entra si es un nuevo departamento
                if (bandera == 'F')
                {
                    Registros registrosTemp = new Registros();
                    registrosTemp.Codigo = cmbDepartamentos.SelectedValue.ToString();
                    registrosTemp.Medicion.Add(txtLluvia.Text);
                    registrosTemp.FechaMedicion.Add(Convert.ToString(DateTime.Now));
                    registros.Add(registrosTemp);
                    Guardar();
                }
            }
        }

        //Guarda en un Archivo de texto
        private void Guardar()
        {
            FileStream stream = new FileStream("Registros.txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter writer = new StreamWriter(stream);

            foreach (var al in registros)
            {
                writer.WriteLine(al.Codigo);

                for (int i = 0; i < al.Medicion.Count; i++)
                {
                    writer.WriteLine(al.Medicion[i]);
                    writer.WriteLine(al.FechaMedicion[i]);
                }
            }
            writer.Close();
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            
            dgvRegistro.DataSource = null;
            dgvRegistro.DataSource = registros;
            dgvRegistro.Refresh();

            if (n == 0)
            {
                //Nueva columna Departamento
                dgvRegistro.Columns.Add("Departamento", "Departamento");

                for (int i = 0; i < 5; i++)
                {
                    dgvRegistro.Columns.Add("Fecha" + i, "Fecha " + (i + 1));
                    dgvRegistro.Columns.Add("Registro" + i, "Registro " + (i + 1));
                }

                //recorrer con un ciclo a todos los registros para ingresarlos a las columnas creadas
                for (int i = 0; i < registros.Count; i++)
                {
                    for (int x = 0; x < 22; x++)
                    {
                        if (registros[i].Codigo == departamentos[x].Codigo)
                        {
                            dgvRegistro["Departamento", i].Value = departamentos[x].Departamento;
                        }
                    }
                    //recorrer con un ciclos las fechas y registros
                    for (int j = 0; j < registros[i].Medicion.Count; j++)
                    {
                        dgvRegistro["Fecha" + j, i].Value = registros[i].FechaMedicion[j];
                        dgvRegistro["Registro" + j, i].Value = registros[i].Medicion[j];
                    }


                }

                n = 1;
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double promedio = ((registros.Count*100.0)/22.0);
            label6.Text = "El promedio de lluvias en Guatemala es: " + promedio + "%";
        }
    }
}
