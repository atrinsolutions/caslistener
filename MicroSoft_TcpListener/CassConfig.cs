using CasListener;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroSoft_TcpListener
{
    class CassConfig : IEntityTypeConfiguration<CasScalePackInfo>
    {
        public void Configure(EntityTypeBuilder<CasScalePackInfo> builder)
        {

        }
    }
}
